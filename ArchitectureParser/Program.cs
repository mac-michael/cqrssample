using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.CRM.Domain;
using CQRS.CRM.Interfaces.Domain.Events;
using CQRS.Sales.Domain;
using CQRS.Sales.Interfaces.Domain.Events;
using CQRS.Shipping.Domain;
using CQRS.Shipping.Interfaces.Domain.Events;

namespace ArchitectureParser
{
    class Program
    {
        static void Main()
        {
            var r = new BuildingBlockRecognizer();
            var p = new DomainProcessor(
                new BoundedContextProcessor(r,
                    new GroupingProcessor("Commands",
                        new CommandProcessor(r)
                        ),
                    new GroupingProcessor("Domain",
                        new AggregateProcessor(r),
                        new ValueObjectProcessor(r),
                        new DomainEventProcessor(r),
                        new RepositoryProcessor(r),
                        new ServiceProcessor(r),
                        new FactoryProcessor(r)),
                    new SagaProcessor(r)
                    ));

            p.AddAssemblyOfType<Invoice>();
            p.AddAssemblyOfType<OrderCreatedEvent>();
            p.AddAssemblyOfType<Customer>();
            p.AddAssemblyOfType<CustomerStatusChangedEvent>();
            p.AddAssemblyOfType<Shipment>();
            p.AddAssemblyOfType<OrderShippedEvent>();
            p.AddAssemblyOfType<Money>();

            XElement xml = p.Parse();


          //  Console.WriteLine(xml);

            var builder = new DomainSvgBuilder();
            var svg = builder.CreateDocument().ToString();
            var script = File.ReadAllText("script.html");
            svg = svg.Insert(svg.IndexOf("</style>")+8 , script);

           // Console.WriteLine(svg);
            File.WriteAllText("a.svg", svg);
        }
    }

    public class DomainSvgBuilder : SvgBuilder
    {
        private XElement GetContent()
        {
            return Group(
                Box(-10, -10, 440, 120, Id("root"), new XAttribute("fill", "none")),
                CreateBC("Sales"),
                CreateBC("Shipment", Translate(150, 0)),
                CreateBC("Crm", Translate(300, 0))
                );
        }

        public XElement CreateBC(string name, params object[] content)
        {
            return CreateBox(name,
                Id(name),
                CreateLayer("Application (API)", Translate(10, 10)),
                CreateLayer("Domain", Translate(10, 38)),
                CreateLayer("Infrastructure", Translate(10, 66)),
                content
            );
        }

        private static int ok;
        public XElement CreateLayer(string name, params object[] content)
        {
            return Group(
                RoundedBox(0, 0, 100, 24, 4, Gradient("layer")),
                Text(50, 5, name, Class("layerTitle")),
                ok++ == 1
                    ? Group(Combine(Translate(10, 10), Scale(0.10)),
                //Viewport(Size(10, 10, 80, 12), new XAttribute("viewBox", "-10 -10 300 120"),
                //       new XAttribute("preserveAspectRatio", "view"),
                               CreateAggregate("Order"),
                               CreateBox("Product", Translate(150, 0))
                          )
                    : null,
                content);
        }


        public XElement CreateAggregate(string name)
        {
            return Group(
                Id("Order"),
                CreateBox2(0, 0, 120, 100, 20, "Order",
                    Group(
                        Scale(0.7),
                        Class("aggregateElements"),
                        CreateBox2(10, 80, 60, 50, 10, "OrderLine", Id("OrderLine"))
                    )
                ),
                CreateBox2(-2, -28, 124, 20, 5, "", Id("Invariant"), Class("invariants", "aggregatePanel"),
                    Text(20, -24, "all invariants"),
                    Text(20, -18, "invariant 1"),
                    Text(20, -12, "ninvariant 2")

                ),

                CreateBox2(-28, -2, 20, 104, 5, "", Class("aggregatePanel")),


                CreateBox2(-2, 108, 124, 20, 5, "", Class("aggregatePanel"),
                    Text(60, 116, "emitted events", Id("Events")),

                    Group(

                        Class("aggregateEvents"),
                        CreateBox2(100, 800, 60, 50, 10, "OrderCreatedEvent", Id("Event")),
                        CreateBox2(200, 800, 60, 50, 10, "OrderSubmittedEvent"),
                        Scale(0.15)
                    )

                )

                );
        }

        public XDocument CreateDocument()
        {
            var ns = XNamespace.Get("http://www.w3.org/2000/svg");
            var ns1 = XNamespace.Get("http://sozi.baierouge.fr");

            return new XDocument(
                new XElement("svg",

                             new XAttribute(XNamespace.Xmlns + "ns1", ns1),

                             Group(
                                 XDocument.Load("defs.xml").Root,
                                 GetContent()
                                 ),
                             new XElement("style",
                                          new XAttribute("type", "text/css"),
                                          new XCData(File.ReadAllText("style.css"))
                                 )
                    ).WithDefaultXmlNamespace(ns));
        }
    }

    public class SvgBuilder
    {
        public XAttribute[] Position(double x, double y)
        {
            return new[] { new XAttribute("x", x), new XAttribute("y", y) };
        }

        public XAttribute[] Size(double x, double y, double width, double height)
        {
            return Position(x,y).Union(Dimension(width,height)).ToArray();
        }

        public XAttribute[] Dimension(double width, double height)
        {
            return new[] { new XAttribute("width", width), new XAttribute("height", height) };
        }

        public XAttribute Combine(params XAttribute[] attributes)
        {
            return new XAttribute(attributes.First().Name, string.Join(" ", from a in attributes
                                                                            select a.Value));
        }

        public XAttribute Scale(double scale)
        {
            return new XAttribute("transform", string.Format("scale({0})", scale.ToString(CultureInfo.InvariantCulture)));
        }

        public XAttribute Translate(double x, double y)
        {
            return new XAttribute("transform", string.Format("translate({0},{1})", 
                x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture)));
        }

        public XAttribute Rotate(double angle, double centerX, double centerY)
        {
            return new XAttribute("transform", string.Format("rotate({0},{1},{2})",
                angle.ToString(CultureInfo.InvariantCulture), centerX.ToString(CultureInfo.InvariantCulture), centerY.ToString(CultureInfo.InvariantCulture)));
        }

        public XAttribute Id(string name)
        {
            return new XAttribute("id",name);
        }

        public XAttribute Gradient(string name)
        {
            return new XAttribute("fill", string.Format("url(#{0})", name));
        }


        public XElement CreateBox2( int x, int y, int width, int height, int line, string name, params object[] content)
        {
            return Group(CreatePolyline(x+line, y, x, y, x, y+height, x+line, y+height),
                         Text(x+width/2, y, name, Class("title")),
                         CreatePolyline(x+width-line, y, x+width, y, x+width, y+height, x+width-line, y+height),
                         content
                );
        }

        public XElement CreateBox( string name, params object[] content)
        {
            return Group(CreatePolyline(20, 0, 0, 0, 0, 100, 20, 100),
                         Text(60, 0, name, Class("title") ),
                         CreatePolyline(100, 0, 120, 0, 120, 100, 100, 100),
                         content
                );
        }

        public XAttribute Class(params string[] cssClasses)
        {
            return new XAttribute("class", string.Join(" ",cssClasses));
        }

        public XElement Box(int x, int y, int width, int height, params object[] content)
        {
            return RoundedBox(x, y, width, height, 0, content);
        }

        public XElement RoundedBox(int x, int y, int width, int height, int radius = 10, params object[] content)
        {
            var roundedBox = new XElement("rect",
                                          new XAttribute("x", x), new XAttribute("y", y),
                                          new XAttribute("width", width), new XAttribute("height", height),
                                          new XAttribute("rx", radius), new XAttribute("ry", radius), content);

            return roundedBox;
        }

        public XElement Group(params object[] elements)
        {
            return new XElement("g", elements);
        }

        public XElement Text(int x, int y, string text, params object[] content)
        {
            return new XElement("text", new XAttribute("x", x), new XAttribute("y", y), text, content);
        }

        public static XElement CreatePolyline(params int[] positions)
        {
            string points = "";

            for (int i = 0; i < positions.Length/2; i++)
                points += string.Format("{0},{1} ", positions[i*2], positions[i*2 + 1]);

            return new XElement("polyline", new XAttribute("points", points));
        }

        public static XElement CreateLine(float x, float y, float x2, float y2)
        {
            return new XElement("line", new XAttribute("x1", x),
                                new XAttribute("y1", y),
                                new XAttribute("x2", x2),
                                new XAttribute("y2", y2));
        }
    }

}

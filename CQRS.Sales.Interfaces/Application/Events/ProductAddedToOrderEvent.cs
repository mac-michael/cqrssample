namespace CQRS.Sales.Interfaces.Application.Events
{
    public class ProductAddedToOrderEvent
    {
        public int Productid { get; private set; }

        public int ClientId { get; private set; }

        public int Quantity { get; private set; }

        public ProductAddedToOrderEvent(int productid, int clientId, int quantity)
        {
            Productid = productid;
            ClientId = clientId;
            Quantity = quantity;
        }

        //public bool equals(Object obj) {
        //    return EqualsBuilder.reflectionEquals(this, obj);
        //}

        //public int hashCode() {
        //    return HashCodeBuilder.reflectionHashCode(this);
        //}

        //public String toString() {
        //    return ToStringBuilder.reflectionToString(this, ToStringStyle.SHORT_PREFIX_STYLE);
        //}
    }
}
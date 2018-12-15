using System;
using System.Collections;
using NUnit.Framework;

namespace CQRS.Tests.Helpers
{
    public static class SpecificationExtensions
    {
        public delegate void MethodThatThrows();

        // Methods
        public static Exception GetException(this MethodThatThrows method)
        {
            Exception exception = null;
            try
            {
                method();
            }
            catch (Exception exception2)
            {
                exception = exception2;
            }
            return exception;
        }

        public static void ShouldBeEmpty(this ICollection collection)
        {
            Assert.IsEmpty(collection);
        }

        public static void ShouldBeEmpty(this string aString)
        {
            Assert.IsEmpty(aString);
        }

        public static string ShouldBeEqualIgnoringCase(this string actual, string expected)
        {
            StringAssert.AreEqualIgnoringCase(expected, actual);
            return expected;
        }

        public static void ShouldBeFalse(this bool condition)
        {
            Assert.IsFalse(condition);
        }

        public static IComparable ShouldBeGreaterThan(this IComparable arg1, IComparable arg2)
        {
            Assert.Greater(arg1, arg2);
            return arg2;
        }

        public static IComparable ShouldBeLessThan(this IComparable arg1, IComparable arg2)
        {
            Assert.Less(arg1, arg2);
            return arg2;
        }

        public static void ShouldBeNull(this object anObject)
        {
            Assert.IsNull(anObject);
        }

        public static void ShouldBeOfType(this object actual, Type expected)
        {
            Assert.IsInstanceOfType(expected, actual);
        }

        public static void ShouldBeSurroundedWith(this string actual, string expectedDelimiter)
        {
            StringAssert.StartsWith(expectedDelimiter, actual);
            StringAssert.EndsWith(expectedDelimiter, actual);
        }

        public static void ShouldBeSurroundedWith(this string actual, string expectedStartDelimiter, string expectedEndDelimiter)
        {
            StringAssert.StartsWith(expectedStartDelimiter, actual);
            StringAssert.EndsWith(expectedEndDelimiter, actual);
        }

        public static object ShouldBeTheSameAs(this object actual, object expected)
        {
            Assert.AreSame(expected, actual);
            return expected;
        }

        public static Exception ShouldBeThrownBy(this Type exceptionType, MethodThatThrows method)
        {
            Exception anObject = method.GetException();
            Assert.IsNotNull(anObject);
            Assert.AreEqual(exceptionType, anObject.GetType());
            return anObject;
        }

        public static void ShouldBeTrue(this bool condition)
        {
            Assert.IsTrue(condition);
        }

        public static void ShouldContain(this IList actual, object expected)
        {
            Assert.Contains(expected, actual);
        }

        public static void ShouldContain(this string actual, string expected)
        {
            StringAssert.Contains(expected, actual);
        }

        public static void ShouldContainErrorMessage(this Exception exception, string expected)
        {
            StringAssert.Contains(expected, exception.Message);
        }

        public static void ShouldEndWith(this string actual, string expected)
        {
            StringAssert.EndsWith(expected, actual);
        }

        public static object ShouldEqual(this object actual, object expected)
        {
            Assert.AreEqual(expected, actual);
            return expected;
        }

        public static void ShouldNotBeEmpty(this ICollection collection)
        {
            Assert.IsNotEmpty(collection);
        }

        public static void ShouldNotBeEmpty(this string aString)
        {
            Assert.IsNotEmpty(aString);
        }

        public static void ShouldNotBeNull(this object anObject)
        {
            Assert.IsNotNull(anObject);
        }

        public static void ShouldNotBeOfType(this object actual, Type expected)
        {
            Assert.IsNotInstanceOfType(expected, actual);
        }

        public static object ShouldNotBeTheSameAs(this object actual, object expected)
        {
            Assert.AreNotSame(expected, actual);
            return expected;
        }

        public static void ShouldNotContain(this IList collection, object expected)
        {
            CollectionAssert.DoesNotContain(collection, expected);
        }

        public static object ShouldNotEqual(this object actual, object expected)
        {
            Assert.AreNotEqual(expected, actual);
            return expected;
        }

        public static void ShouldStartWith(this string actual, string expected)
        {
            StringAssert.StartsWith(expected, actual);
        }
    }
}
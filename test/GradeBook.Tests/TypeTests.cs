using System;
using Xunit;

namespace GradeBook.Tests
{
    public class TypeTests
    {

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            var name = "Mike";
            var upper = MakeUpper(name);

            Assert.Equal("Mike", name);
            Assert.Equal("MIKE", upper);

        }

        String MakeUpper(String s)
        {
            return s.ToUpper();
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt(3);
            SetInt(ref x, 42);

            Assert.Equal(42, x);
        }

        void SetInt(ref int x, int newVal)
        {
            x = newVal;
        }
        int GetInt(int newVal)
        {
            return newVal;
        }

        [Fact]
        public void CSCanPassedByRef()
        {
            var book1 = GetBook("book 1");
            GetBookSetName(ref book1, "New name");

            Assert.Equal("New name", book1.Name);

        }

        void GetBookSetName(ref Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void CSIsPassedByValue()
        {
            var book1 = GetBook("book 1");
            GetBookSetName(book1, "New name");

            Assert.Equal("book 1", book1.Name);

        }

        void GetBookSetName(Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("book 1");
            SetName(book1, "New name");

            Assert.Equal("New name", book1.Name);

        }

        void SetName(Book book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("book 1");
            var book2 = GetBook("book 2");

            Assert.Equal("book 1", book1.Name);
            Assert.Equal("book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarCanReffrenceSameObject()
        {
            var book1 = GetBook("book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        Book GetBook(string name)
        {
            return new Book(name);
        }

    }
}

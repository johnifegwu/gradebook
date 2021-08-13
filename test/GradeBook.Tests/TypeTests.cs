using System;
using Xunit;

namespace GradeBook.Tests
{
    public class TypeTests
    {

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

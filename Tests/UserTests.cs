using System;
using Xunit;
using Domain.Entities;

namespace Tests
{
    public class UserTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("1234")]
        [InlineData(" Rafael")]
        [InlineData("Rafael ")]
        [InlineData("Rafael  ")]
        [InlineData("Rafael I")]
        [InlineData("Rafael 0")]
        [InlineData("Rafael --")]
        [InlineData("R4fael Rodrigues")]
        [InlineData("Rafael Rodrigues Fernande$")]
        [InlineData("Raf@el Rodrigues")]
        public void Should_return_false_when_name_is_invalid(string name)
        {
            var user = new User(name, "084.538.989-01", "29/06/1992", "fulano@gmail.com", "47996034561");

            var userIsValid = user.Validate().isValid;

            Assert.False(userIsValid);
        }

        [Theory]
        [InlineData("Maria Pereira")]
        [InlineData("Fernando Fernandes da Silva")]
        [InlineData("Ana Sá")]
        [InlineData("Frederico Pereira Guimarães da Cunha Mourão Albuquerque")]
        public void Should_return_true_when_name_is_valid(string name)
        {
            var user = new User(name, "084.538.989-01", "29/06/1992", "fulano@gmail.com", "47996034561");

            var userIsValid = user.Validate().isValid;

            Assert.True(userIsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("000.000.000-00")]
        [InlineData("000.000.000-01")]
        [InlineData("100.000.000-00")]
        [InlineData("999.999.999-99")]
        [InlineData("000.368.560-00")]
        [InlineData("640.3685606")]
        [InlineData("640.368.560-6")]
        [InlineData("640.368.560-6a")]
        [InlineData("640.368.560-061")]
        [InlineData("640368560061")]
        public void Should_return_false_when_document_is_invalid(string personalDocument)
        {
            // Dado / Setup
            User user = new User("Maria Pereira", personalDocument, "29/06/1992", "fulano@gmail.com", "47996034561");

            // When / Ação
            bool docIsValid = user.Validate().isValid;
            
            // Deve / Asserções
            Assert.False(docIsValid);
        }

        [Theory]
        [InlineData("493.107.310-79")]
        [InlineData("700.651.710-98")]
        [InlineData("104.632.250-82")]
        [InlineData("830.374.420-85")]
        [InlineData("110.792.660-20")]
        [InlineData("11079266020")]
        public void Should_return_true_when_document_is_valid(string personalDocument)
        {
            // Dado / Setup
            User user = new User("Maria Pereira", personalDocument, "29/06/1992", "fulano@gmail.com", "4733398336");

            // When / Ação
            bool docIsValid = user.Validate().isValid;
            
            // Deve / Asserções
            Assert.True(docIsValid);
        }

        [Theory]
        [InlineData("47996034561")]
        [InlineData("(47)996034561")]
        [InlineData("47 996034561")]
        [InlineData("47 99603-4561")]
        [InlineData("47996034561")]
        [InlineData("47.33398336")]
        [InlineData("47 33398336")]
        [InlineData("(47)33398336")]
        [InlineData("473339-8336")]
        public void Should_return_true_when_phone_is_valid(string phone)
        {
            // Dado / Setup
            User user = new User("Maria Pereira", "084.538.989-01", "29/06/1992", "fulano@gmail.com", phone);

            // When / Ação
            bool phoneIsValid = user.Validate().isValid;
            
            // Deve / Asserções
            Assert.True(phoneIsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("473339")]
        [InlineData("4796034561")]
        [InlineData("(47*996034561")]
        [InlineData("47L96034561")]
        [InlineData("47 99F03-4561")]
        [InlineData("479960394561")]
        [InlineData("47 833398336")]
        public void Should_return_false_when_phone_is_invalid(string phone)
        {
            // Dado / Setup
            User user = new User("Maria Pereira", "084.538.989-01", "29/06/1992", "fulano@gmail.com", phone);

            // When / Ação
            bool phoneIsValid = user.Validate().isValid;
            
            // Deve / Asserções
            Assert.False(phoneIsValid);
        }
    }
}

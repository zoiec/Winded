using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindSystem.DataModels.HumanResources;
using Xunit;
using Xunit.Extensions;

namespace NorthWindSystem.Requirements
{
    public class When_Managing_Employees
    {
        // 
        [AutoRollback]
        [Theory]
        //[InlineData(DateTime.Today.AddDays(1), "Future birthdates not allowed")]
        //[InlineData(DateTime.Today.AddYears(-18).AddDays(1), "Must be at least 18")]
        public void Should_Reject_Invalid_BirthDate(DateTime birthDate, string expectedExceptionMessage)
        {
            // Arrange
            var expected = new Employee()
            {
                FirstName = "Bill",
                LastName = "Balmer",
                BirthDate = birthDate
            };
            // Act

            // Assert
            throw new NotImplementedException();
        }

        // Should_Accept_New_Employee
        // Should_Transfer_Territories
        // Should_Reject_Circular_Org_Chart
    }
}

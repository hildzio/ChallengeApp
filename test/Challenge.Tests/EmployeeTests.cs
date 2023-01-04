using System;
using ChallengeApp;
using Xunit;

namespace EmployeeTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // arrange
            var emp = new Employee("Adam");
            emp.AddGrade(24.4);
            emp.AddGrade(36.6);
            var emp1 = new Employee("Ewa");
            emp1.AddGrade(10);
            emp1.AddGrade(20);
            emp1.AddGrade(30);
            var emp2 = new Employee("Janusz");
            emp2.AddGrade(4.23);
            emp2.AddGrade(3.38);
            emp2.AddGrade(18.27);
            emp2.AddGrade(14.29);
            // act
            var result = emp.GetStatistics();
            var result1 = emp1.GetStatistics();
            var result2 = emp2.GetStatistics();
            // assert
            Assert.Equal(30.5, result.Average, 1);
            Assert.Equal(36.6, result.High);
            Assert.Equal(24.4, result.Low);
            Assert.Equal(20, result1.Average, 1);
            Assert.Equal(30, result1.High);
            Assert.Equal(10, result1.Low);
            Assert.Equal(10.04, result2.Average, 1);
            Assert.Equal(3.38, result2.Low);
            Assert.Equal(18.27, result2.High);
        }
    }
}
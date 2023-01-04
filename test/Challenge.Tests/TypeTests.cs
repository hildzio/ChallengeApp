using System;
using ChallengeApp;
using Xunit;

namespace Challenge.Tests
{
    public class TypeTests
    {
        [Fact]
        public void GetEmployeeReturnsDifferentObjects()
        {
            var emp1 = GetEmployee("Adam");
            var emp2 = GetEmployee("Tomek");
            var emp3 = GetEmployee("Magda");
            Assert.NotSame(emp1, emp2);
            Assert.False(Object.ReferenceEquals(emp1, emp3));
        }
        private Employee GetEmployee(string name)
        {
            return new Employee(name);
        }

        [Fact]
        public void GetEmployeeReturnsTheSameOjects()
        {
            var emp1 = GetEmployee1("Adam");
            var emp2 = GetEmployee1("Tomek");
            var emp3 = GetEmployee1("Magda");
            var emp4 = emp1;
            Assert.Equal("Magda", emp3.Name);
            Assert.Same(emp1, emp4);
            Object.ReferenceEquals("Tomek", emp2);
        }
        private Employee GetEmployee1(string name)
        {
            return new Employee(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var emp10 = GetEmployee("Adam");
            this.SetName(out emp10, "NewName");
            Assert.Equal("NewName", emp10.Name);
        }
        private void SetName(out Employee employee, string name)
        {
            employee = new Employee("NEW");
            employee.name = name;
        }
        [Fact]
        public void CSharpCanPassByRef()
        {
            Employee emp1 = GetEmployee("Employee 1");
            GetEmployeeSetName(out emp1, "New Name");
            Assert.Equal("New Name", emp1.Name);
        }
        private void GetEmployeeSetName(out Employee emp, string name)
        {
            emp = new Employee(name);
        }
        [Fact]
        public void StringBehaveLikeValueType()
        {
            var x = "Adam";
            var upper = this.MakeUppercase(x);
            Assert.Equal("Adam", x);
            Assert.Equal("ADAM", upper);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }
        //[Fact]
        //  private void GetEmployeeSetName(out Employee emp, string name);
    }
}
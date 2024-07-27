
using HW31DataBaseTest.Models;

namespace HW31DataBaseTest.Tests
{
    public class Tests : BaseTest
    {
        [Test]
        public void CRUDTest1()
        {
            var group = GetGroup(2);
            var student = GetStudent(group);

            //checking connection
            Assert.IsTrue(dbHelper.IsDataBaseConnection());

            //Delete group duplicate
            dbHelper.DeleteRow("GroupList", "idGroupList", $"{group.idGroupList}");

            //Create new group and student
            dbHelper.CreateNewGroup(group.Name, group.idGroupList);
            dbHelper.CreateNewStudent(student.Name, student.IdGroupList);

            //checking is student exist
            Assert.True(dbHelper.IsUserExist(student));

            //update student by name
            var newStudentName = RandomString();
            dbHelper.UpdateUser("Name", newStudentName, "idUsers", $"{dbHelper.GetStudentId(student)}");
            student.Name = newStudentName;

            //checking is value changed
            Assert.True(dbHelper.IsUserExist(student));

            //delete user
            dbHelper.DeleteRow("Users", "Name", student.Name);

            //checking is usere deleted
            Assert.False(dbHelper.IsUserExist(student));
        }
    }
}
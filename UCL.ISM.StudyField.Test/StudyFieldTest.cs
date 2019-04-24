using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FakeItEasy;

namespace UCL.ISM.StudyField.Test
{
    public class StudyFieldTest
    {       
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void CreateNewStudyField_Fail(string input)
        {
            //Arrange
            var studyField = A.Fake<IStudyField>();

            A.CallTo(() => studyField.CreateNewStudyField(input))
            .Throws(new ArgumentException("Study name was not in correct format."));
        }

        [Theory]
        [InlineData(" Louise")]
        [InlineData(" Hans ")]
        [InlineData("Peter")]
        public void CreateNewStudyField_Pass(string input)
        {

        }
    }
}

using Microsoft.Extensions.Time.Testing;

namespace BunkersAndBluffs.Game.Application.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void expiredCode()
        {
            //string Code = GenerateCode();
            FakeTimeProvider TestTime = new FakeTimeProvider(DateTimeOffset.Now.AddMinutes(21));
            // Assert.Equal(false, IsCodeCorrect(Code, TestTime));
        }
    }
}

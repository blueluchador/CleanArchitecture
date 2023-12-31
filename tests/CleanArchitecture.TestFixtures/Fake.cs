using AutoFixture;
using AutoFixture.Dsl;

namespace CleanArchitecture.TestFixtures;

public static class Fake
{
    public static T Create<T>()
    {
        var fixture = new Fixture();
        return fixture.Create<T>();
    }

    public static IEnumerable<T> CreateMany<T>()
    {
        var fixture = new Fixture();
        return fixture.CreateMany<T>();
    }
    
    public static IEnumerable<T> CreateMany<T>(int count)
    {
        var fixture = new Fixture();
        return fixture.CreateMany<T>(count);
    }

    public static ICustomizationComposer<T> Build<T>()
    {
        var fixture = new Fixture();
        return fixture.Build<T>();
    }
}
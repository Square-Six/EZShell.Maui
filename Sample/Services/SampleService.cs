namespace Sample.Interfaces;

public interface ISampleService
{
    string Test();
}

public class SampleService : ISampleService
{
    public string Test()
    {
        return "Value from Dependency Injection";
    }
}
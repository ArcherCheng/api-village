namespace Api.Services;

public class BaseDto
{
    // public virtual Guid NewGuid()
    // {
    //     return new Guid(); 
    // }

    public virtual object GetPropertyValue(string name)
    {
       return this.GetType().GetProperty(name)?.GetValue(this)!;
    }
}
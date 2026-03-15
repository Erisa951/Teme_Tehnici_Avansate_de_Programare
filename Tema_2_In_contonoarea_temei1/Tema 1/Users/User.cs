namespace Tema_1.Users;

public abstract class User
{
    public int Id { get; }

    public string Name { get; }

    
    protected User(int id, string name)
    {
        Id = id;
        Name = name;
    }

   
    public abstract string GetRole();
}
namespace Base.Domain;

public class LangStr: Dictionary<string, string>
{
    private const string DefaultCulture = "en";
    
    public LangStr()
    {
    }

    public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
    }

    public LangStr(string value, string culture)
    {
        this[culture] = value;
    }

    public override string ToString()
    {
        return Translate() ?? "Translation wasn't found";
    }

    public string? Translate(string? culture = null)
    {
        if (Count == 0) return null;

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        Console.WriteLine(culture);

        if (ContainsKey(culture))
        {
            return this[culture];
        }

        var neutralCulture = culture.Split("-")[0];

        if (ContainsKey(neutralCulture))
        {
            return this[neutralCulture];
        }

        if (ContainsKey(DefaultCulture))
        {
            return this[DefaultCulture];
        }

        return null;

        // object - query
        // en-GB - en-GB => done
        // en - en-GB => TODO
        // en-GB - en-US => TODO
        // en-GB - ru => TODO
        // null - ru => TODO
    }

    public void SetTranslation(string value)
    {
        this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    }

    // string test = new LangStr("tst")
    public static implicit operator string(LangStr? langStr) => langStr?.ToString()
                                                                ?? "null";
    // LangStr lstr = "test"; // Internally it will be lStr = new Langstr("test");
    public static implicit operator LangStr(string value) => new (value);
}
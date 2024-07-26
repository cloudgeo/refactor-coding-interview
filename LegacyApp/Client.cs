using System;

namespace LegacyApp
{
    public class Client
    {
        public int Id { get; set; }
        public SpecialClientCategory SpecialClientCategory { get; set; }
        public ClientStatus ClientStatus { get; set; }
    }

    public enum SpecialClientCategory {
		[SpecialClientCategoryMultiplier(1)]
		Unknown,
		[SpecialClientCategoryMultiplier(2)]
		VeryImportantClient,
		[SpecialClientCategoryMultiplier(1)]
		ImportantClient,
		[SpecialClientCategoryMultiplier(4)]
		SuperClient
    }

    public class SpecialClientCategoryMultiplierAttribute : Attribute {
	    public int Multiplier { get; }

	    public SpecialClientCategoryMultiplierAttribute(int multiplier) {
		    Multiplier = multiplier;
	    }
    }
}

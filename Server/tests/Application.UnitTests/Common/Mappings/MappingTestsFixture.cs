using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;

namespace CookingRecipesSystem.Application.UnitTests.Common.Mappings
{
	public class MappingTestsFixture
	{
		public MappingTestsFixture()
		{
			this.ConfigurationProvider = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});

			this.Mapper = this.ConfigurationProvider.CreateMapper();
		}

		public IConfigurationProvider ConfigurationProvider { get; }

		public IMapper Mapper { get; }
	}
}

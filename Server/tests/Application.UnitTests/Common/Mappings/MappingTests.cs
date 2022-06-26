using AutoMapper;

namespace CookingRecipesSystem.Application.UnitTests.Common.Mappings
{
	public class MappingTests : IClassFixture<MappingTestsFixture>
	{
		private readonly IConfigurationProvider configuration;
		private readonly IMapper mapper;

		public MappingTests(MappingTestsFixture fixture)
		{
			this.configuration = fixture.ConfigurationProvider;
			this.mapper = fixture.Mapper;
		}

		//[Theory]
		//[InlineData(typeof(Model), typeof(OutputModel))]
		//public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
		//{
		//	var instance = Activator.CreateInstance(source);

		//	this.mapper.Map(instance, source, destination);
		//}
	}
}

using System.Text;

using AutoFixture;

using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Commands.Create;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.AspNetCore.Http;

using Moq;

using Shouldly;

using static CookingRecipesSystem.Application.Ingredients.Commands.Create.IngredientCreateCommand;

namespace CookingRecipesSystem.Application.UnitTests.Ingredients.Commands.Create
{
	public class IngredientCreateCommandTests
	{
		private IFormFile GetIFormImageFile()
		{
			string content = "File content.";
			byte[] bytes = Encoding.Unicode.GetBytes(content);
			var iFormFileFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpeg");

			return iFormFileFile;
		}

		private IngredientCreateCommandHandler MockIngredientCreateCommandHandler(
			PhotoResponseModel photoResponseModel,
			Photo photoFile,
			Ingredient ingredient,
			int writtenEntries)
		{
			var ingredientList = new List<Ingredient>();
			ingredientList.Add(ingredient);

			var mockedMapper = new Mock<IMapper>();
			mockedMapper
				.Setup(x => x.Map<Photo>(It.IsAny<PhotoResponseModel>()))
				.Returns(photoFile);
			mockedMapper
				.Setup(x => x.Map<Ingredient>(It.IsAny<IngredientRequestModel>()))
				.Returns(ingredient);

			var mockedPhotoService = new Mock<IPhotoService>();
			mockedPhotoService
				.Setup(x => x.Process(It.IsAny<IFormFile>(), CancellationToken.None))
				.Returns(Task.FromResult(photoResponseModel));

			var mockedPhotoRepository = new Mock<IAppRepository<Photo>>();
			mockedPhotoRepository
				.Setup(x => x.Create(It.IsAny<Photo>(), CancellationToken.None))
				.Returns(Task.FromResult(photoFile));

			var mockedIngredientRepository = new Mock<IAppRepository<Ingredient>>();

			mockedIngredientRepository
				.Setup(x => x.Create(ingredient, CancellationToken.None))
				.Returns(Task.FromResult(ingredient));
			mockedIngredientRepository
				.Setup(x => x.SaveAsync(CancellationToken.None))
				.Returns(Task.FromResult(writtenEntries));

			var handler = new IngredientCreateCommand
				.IngredientCreateCommandHandler(
				mockedPhotoService.Object,
				mockedIngredientRepository.Object,
				mockedPhotoRepository.Object,
				mockedMapper.Object);

			return handler;
		}

		[Fact]
		public async Task Handle_Should_Persist_Ingredient()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = fixture.Create<string>();
			var photoResponseModel = fixture.Create<PhotoResponseModel>();
			var photoFile = fixture.Create<Photo>();
			var ingredient = fixture.Create<Ingredient>();
			var iFormFileFile = GetIFormImageFile();
			var writtenEntries = 1; // 1 entity is save in database.

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var handler = MockIngredientCreateCommandHandler(
				photoResponseModel,
				photoFile,
				ingredient,
				writtenEntries);

			//Act
			var result = await handler.Handle(command, CancellationToken.None);

			//Assert
			result.Succeeded.ShouldBe(true);
			result.Errors.ShouldBeEmpty();
			result.Response.Id.ShouldBe(ingredient.Id);
		}

		[Fact]
		public async Task Handle_Should_Return_Properly_Result_When_Ingredient_Is_Not_Save()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = fixture.Create<string>();
			var photoResponseModel = fixture.Create<PhotoResponseModel>();
			var photoFile = fixture.Create<Photo>();
			var ingredient = fixture.Create<Ingredient>();
			var iFormFileFile = GetIFormImageFile();
			var writtenEntries = 0; // 0 entity is save in database.

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var handler = MockIngredientCreateCommandHandler(
				photoResponseModel,
				photoFile,
				ingredient,
				writtenEntries);

			//Act
			var result = await handler.Handle(command, CancellationToken.None);

			//Assert
			result.Succeeded.ShouldBe(false);
			result.Errors.ShouldContain(ExceptionMessages.IngredientNotCreated);
			result.Response.ShouldBeNull();
		}
	}
}

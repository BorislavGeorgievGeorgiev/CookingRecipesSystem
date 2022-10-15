using AutoFixture;

using CookingRecipesSystem.Application.Ingredients.Commands.Create;
using CookingRecipesSystem.Domain.Common.Constants;

using Microsoft.AspNetCore.Http;

using Shouldly;

namespace CookingRecipesSystem.Application.UnitTests.Ingredients.Commands.Create
{
	public class IngredientCreateCommandValidatorTests : CommandTestBase
	{
		[Fact]
		public void IsValid_Should_Be_True_When_Name_Is_Not_Null_Or_Empty()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = fixture.Create<string>();
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(true);
		}

		[Fact]
		public void IsValid_Should_Be_False_When_Name_Is_Null()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = null;
			string ingredientDescription = fixture.Create<string>();
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(false);
		}

		[Fact]
		public void IsValid_Should_Be_False_When_Name_Is_Empty()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = string.Empty;
			string ingredientDescription = fixture.Create<string>();
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(false);
		}

		[Fact]
		public void IsValid_Should_Be_False_When_Name_Is_More_Than_IngredientNameMaxLength_Symbols()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = new string('a', EntityConstants.IngredientNameMaxLength + 1);
			string ingredientDescription = fixture.Create<string>();
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(false);
		}

		//=======================================================

		[Fact]
		public void IsValid_Should_Be_True_When_Description_Is_Not_Null_Or_Empty()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = fixture.Create<string>();
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(true);
		}

		[Fact]
		public void IsValid_Should_Be_False_When_Description_Is_Null()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = null;
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(false);
		}

		[Fact]
		public void IsValid_Should_Be_False_When_Description_Is_Empty()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = string.Empty;
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(false);
		}

		[Fact]
		public void IsValid_Should_Be_False_When_Description_Is_More_Than_IngredientDescriptionMaxLength_Symbols()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = new string('a', EntityConstants.IngredientDescriptionMaxLength + 1);
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(false);
		}

		//=======================================================

		[Fact]
		public void IsValid_Should_Be_True_When_PhotoFile_Is_Not_Null()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = fixture.Create<string>();
			var iFormFileFile = GetIFormImageFile();

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(true);
		}

		[Fact]
		public void IsValid_Should_Be_False_When_PhotoFile_Is_Null()
		{
			//Arrange
			Fixture fixture = new Fixture();
			string ingredientName = fixture.Create<string>();
			string ingredientDescription = fixture.Create<string>();
			IFormFile? iFormFileFile = null;

			var command = new IngredientCreateCommand
			{
				Name = ingredientName,
				Description = ingredientDescription,
				PhotoFile = iFormFileFile,
			};

			var validator = new IngredientCreateCommandValidator();

			//Act
			var result = validator.Validate(command);

			//Assert
			result.IsValid.ShouldBe(false);
		}
	}
}

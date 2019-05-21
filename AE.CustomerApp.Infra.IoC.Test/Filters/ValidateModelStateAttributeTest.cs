using System;
using System.Collections.Generic;
using AE.CustomerApp.Infra.IoC.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace AE.CustomerApp.Infra.IoC.Test.Filters
{
    public class ValidateModelStateAttributeTest
    {
        private ValidateModelStateAttribute _validateModelStateAttribute;

        public ValidateModelStateAttributeTest()
        {
            _validateModelStateAttribute = new ValidateModelStateAttribute();
        }

        [Trait("ValidateModelStateAttribute", "ValidModelState")]
        [Fact(DisplayName = "ValidateModelStateAttribute model state is valid")]
        public void ValidateModelStateAttributeTest_ValidModelState_DoesNotReturnBadRequest400Response()
        {
            // Arrange
            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                Mock.Of<ModelStateDictionary>()
            );
            var mockActionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<ControllerBase>()
            );

            // Act
            _validateModelStateAttribute.OnActionExecuting(mockActionExecutingContext);

            // Assert
            var mockResult = mockActionExecutingContext.Result;
            Assert.IsNotType<BadRequestObjectResult>(mockResult);
            Assert.Null(mockResult);
        }

        [Trait("ValidateModelStateAttribute", "InvalidModelState")]
        [Fact(DisplayName = "ValidateModelStateAttribute invalid model state returns Bad request 400 response")]
        public void ValidateModelStateAttributeTest_InvalidModelState_ReturnsBadRequest400Response()
        {
            // Arrange
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("fakeName", "fakeName is invalid");
            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );
            var mockActionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<ControllerBase>()
            );

            // Act
            _validateModelStateAttribute.OnActionExecuting(mockActionExecutingContext);

            // Assert
            Assert.IsType<BadRequestObjectResult>(mockActionExecutingContext.Result);
        }
    }
}

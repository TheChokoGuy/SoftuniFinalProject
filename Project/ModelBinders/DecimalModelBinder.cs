using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Project.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if(valueProviderResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueProviderResult.FirstValue))
            {
                decimal actualValue = 0M;
                bool success = false;

                try
                {
                    string decValue = valueProviderResult.FirstValue;
                    decValue = decValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decValue = decValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    actualValue = Convert.ToDecimal(decValue, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }

                if(success)
                {

                    bindingContext.Result = ModelBindingResult.Success(actualValue);
                }
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}

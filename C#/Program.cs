using Microsoft.ML.OnnxRuntimeGenAI;

namespace Repro
{
    internal class Program
    {
        static OgaHandle CreateOgaHandle()
        {
			Console.WriteLine("Creating OgaHandle. . .");
            return new OgaHandle();
		}

		static void DisposeOgaHandle(in OgaHandle handle)
		{
			Console.WriteLine("Disposing OgaHandle. . .");
			handle.Dispose();
		}

        static Model LoadRandomGPT2()
        {
			Console.WriteLine("Loading Random GPT-2. . .");
			Config config;
			try
			{
				config = new("tiny-random-gpt2-fp32/");
			}
			catch (OnnxRuntimeGenAIException e)
			{
				Console.WriteLine("An error ocurred while creating OgaConfig. Ensure that Random GPT-2 is placed in the working directory.");
				Console.WriteLine(e.Message);
				throw;
			}
			Model model = new(config);
			config.Dispose();
			return model;
		}
		static void UnloadModel(in Model model)
		{
			Console.WriteLine("Unloading Random GPT-2. . .");
			model.Dispose();
		}

        static void Main(string[] args)
        {
			// Creates the necessary objects for loading the models
            OgaHandle handle = CreateOgaHandle();
			Model model = LoadRandomGPT2();

			// Unloads everything
			UnloadModel(model);
			DisposeOgaHandle(handle);

			// Recreates everything
			handle = CreateOgaHandle();
			model = LoadRandomGPT2(); // Crash here

			// Unloads everything again
			UnloadModel(model);
			DisposeOgaHandle(handle);
		}
    }
}

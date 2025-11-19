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
			using Config modelConfig = new("tiny-random-gpt2-fp32/");
			return new Model(modelConfig);
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

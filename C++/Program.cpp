#include <iostream>
#include <memory>
#include <stdexcept>
#include "ort_genai.h"

static OgaHandle* CreateOgaHandle()
{
    std::cout << "Creating OgaHandle. . ." << std::endl;
    return new OgaHandle();
}

static void DestroyOgaHandle(OgaHandle*& handle)
{
    std::cout << "Destroying OgaHandle. . ." << std::endl;
    delete handle;
    handle = nullptr;
}

static std::unique_ptr<OgaModel> LoadRandomGPT2()
{
    std::cout << "Loading Random GPT2. . ." << std::endl;
    std::unique_ptr<OgaConfig> config;
    try
    {
        config = OgaConfig::Create("tiny-random-gpt2-fp32/");
    }
    catch (std::runtime_error& e)
    {
        std::cout << "An error ocurred while creating OgaConfig. Ensure that Random GPT-2 is placed in the working directory." << std::endl;
        std::cerr << e.what() << std::endl;
        throw e;
    }
    return OgaModel::Create(*config);
}

static void UnloadModel(std::unique_ptr<OgaModel>& model)
{
    std::cout << "Unloading Random GPT-2" << std::endl;
    model.reset();
}

int main()
{
    // Creates the necessary objects for loading the models
    OgaHandle* handle = CreateOgaHandle();
    auto model = LoadRandomGPT2();

    // Unloads everything
    UnloadModel(model);
    DestroyOgaHandle(handle);

    // Recreates everything
    handle = CreateOgaHandle();
    model = LoadRandomGPT2(); // Crash here

    // Unloads everything again
    UnloadModel(model);
    DestroyOgaHandle(handle);
}

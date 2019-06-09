using OregoBlink.core;

namespace OregoBlink.interactor
{
    public abstract class OregoInteractor
    {
        protected readonly OregoApplication application;

        protected OregoInteractor(OregoApplication application)
        {
            this.application = application;
        }
    }
}
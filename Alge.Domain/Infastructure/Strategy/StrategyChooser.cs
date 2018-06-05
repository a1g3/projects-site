using System;
using System.Collections.Generic;
using System.Linq;
using Alge.Domain.Interfaces.Infastructure;

namespace Alge.Domain.Interfaces.Strategy
{
    public abstract class StrategyChooser<TStrategies, TStrategy> : IStrategyChooser<TStrategies, TStrategy>
    {
        public List<TStrategy> Strategies { get; set; }
        private TStrategy CurrentService { get; set; }

        public StrategyChooser(List<TStrategy> strategies)
        {
            this.Strategies = strategies;    
        }

        protected TStrategy Service {
            get {
                if (CurrentService == null) throw new NotSupportedException();
                return this.CurrentService;
            }
        }

        public void SetStrategy(TStrategies strategies)
        {
            this.CurrentService = this.Strategies.SingleOrDefault();
        }
    }
}

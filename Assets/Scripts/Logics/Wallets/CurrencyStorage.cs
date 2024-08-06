using Services.locator;
using System;
using UnityEngine;

namespace Logics.Wallets
{
    public class CurrencyStorage : MonoBehaviour, InjectDependency
    {
        public event Action<int, int> Change;

        [field: SerializeField] public int Count { get; private set; }

        public void Inject(IServicesLocator locator)
        {

        }

        public void Add(int amount)
        {
            if (amount > 0)
            {
                int countTemp = Count;
                Count += amount;
                Change?.Invoke(countTemp, Count);
            }
        }
    }
}
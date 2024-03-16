﻿using AutoFixture;
using NutritionAdvisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionAdvisor.Tests.Dummy
{
    public static class DummyValueGenerator
    {
        private static readonly Fixture _fixture;

        static DummyValueGenerator()
        {
            _fixture = new Fixture();
            // Always resolve the same goal
            _fixture.Register(() => Goal.BecomeFit);
        }

        public static T Any<T>() => _fixture.Create<T>();
    }
}

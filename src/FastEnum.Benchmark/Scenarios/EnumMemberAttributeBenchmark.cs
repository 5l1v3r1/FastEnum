﻿using System;
using System.Reflection;
using System.Runtime.Serialization;
using BenchmarkDotNet.Attributes;
using FastEnum.Benchmark.Models;



namespace FastEnum.Benchmark.Scenarios
{
    public class EnumMemberAttributeBenchmark
    {
        private const Fruits Value = Fruits.Apple;


        [GlobalSetup]
        public void Setup()
        {
            _ = Enum.GetNames(typeof(Fruits));
            _ = FastEnum<Fruits>.Names;
        }


        [Benchmark(Baseline = true)]
        public string NetCore()
        {
            var type = typeof(Fruits);
            var name = Enum.GetName(type, Value);
            var info = type.GetField(name);
            var attr = info.GetCustomAttribute<EnumMemberAttribute>();
            return attr?.Value;
        }


        [Benchmark]
        public string FastEnum()
            => Value.GetEnumMemberValue();
    }
}
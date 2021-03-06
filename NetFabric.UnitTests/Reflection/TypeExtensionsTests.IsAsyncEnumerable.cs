﻿using NetFabric.TestData;
using System;
using Xunit;

namespace NetFabric.Reflection.UnitTests
{
    public partial class TypeExtensionsTests
    {
        [Theory]
        [MemberData(nameof(DataSets.AsyncEnumerables), MemberType = typeof(DataSets))]
        public void IsAsyncEnumerable_Should_ReturnTrue(Type type, Type getAsyncEnumeratorDeclaringType, int getAsyncEnumeratorParametersCount, Type currentDeclaringType, Type moveNextAsyncDeclaringType, Type disposeAsyncDeclaringType, Type itemType)
        {
            // Arrange

            // Act
            var result = type.IsAsyncEnumerable(out var enumerableInfo);

            // Assert   
            Assert.True(result);

            Assert.NotNull(enumerableInfo.GetAsyncEnumerator);
            Assert.Equal("GetAsyncEnumerator", enumerableInfo.GetAsyncEnumerator.Name);
            Assert.Equal(getAsyncEnumeratorDeclaringType, enumerableInfo.GetAsyncEnumerator.DeclaringType);
            Assert.Equal(getAsyncEnumeratorParametersCount, enumerableInfo.GetAsyncEnumerator.GetParameters().Length);

            var enumeratorInfo = enumerableInfo.EnumeratorInfo;

            Assert.NotNull(enumeratorInfo.Current);
            Assert.Equal("Current", enumeratorInfo.Current.Name);
            Assert.Equal(currentDeclaringType, enumeratorInfo.Current.DeclaringType);
            Assert.Equal(itemType, enumeratorInfo.Current.PropertyType);

            Assert.NotNull(enumeratorInfo.MoveNextAsync);
            Assert.Equal("MoveNextAsync", enumeratorInfo.MoveNextAsync.Name);
            Assert.Equal(moveNextAsyncDeclaringType, enumeratorInfo.MoveNextAsync.DeclaringType);
            Assert.Empty(enumeratorInfo.MoveNextAsync.GetParameters());

            if (disposeAsyncDeclaringType is null)
            {
                Assert.Null(enumeratorInfo.DisposeAsync);
            }
            else
            {
                Assert.NotNull(enumeratorInfo.DisposeAsync);
                Assert.Equal("DisposeAsync", enumeratorInfo.DisposeAsync.Name);
                Assert.Equal(disposeAsyncDeclaringType, enumeratorInfo.DisposeAsync.DeclaringType);
                Assert.Empty(enumeratorInfo.DisposeAsync.GetParameters());
            }
        }

        [Theory]
        [MemberData(nameof(DataSets.InvalidAsyncEnumerables), MemberType = typeof(DataSets))]
        public void IsAsyncEnumerable_With_MissingFeatures_Should_ReturnFalse(Type type, Type getAsyncEnumeratorDeclaringType, int getAsyncEnumeratorParametersCount, Type currentDeclaringType, Type moveNextAsyncDeclaringType, Type disposeAsyncDeclaringType, Type itemType)
        {
            // Arrange

            // Act
            var result = type.IsAsyncEnumerable(out var enumerableInfo);

            // Assert   
            Assert.False(result);

            if (getAsyncEnumeratorDeclaringType is null)
            {
                Assert.Null(enumerableInfo.GetAsyncEnumerator);
            }
            else
            {
                Assert.NotNull(enumerableInfo.GetAsyncEnumerator);
                Assert.Equal("GetAsyncEnumerator", enumerableInfo.GetAsyncEnumerator.Name);
                Assert.Equal(getAsyncEnumeratorDeclaringType, enumerableInfo.GetAsyncEnumerator.DeclaringType);
                Assert.Equal(getAsyncEnumeratorParametersCount, enumerableInfo.GetAsyncEnumerator.GetParameters().Length);
            }

            var enumeratorInfo = enumerableInfo.EnumeratorInfo;
            if (currentDeclaringType is null)
            {
                Assert.Null(enumeratorInfo.Current);
            }
            else
            {
                Assert.NotNull(enumeratorInfo.Current);
                Assert.Equal("Current", enumeratorInfo.Current.Name);
                Assert.Equal(currentDeclaringType, enumeratorInfo.Current.DeclaringType);
                Assert.Equal(itemType, enumeratorInfo.Current.PropertyType);
            }

            if (moveNextAsyncDeclaringType is null)
            {
                Assert.Null(enumeratorInfo.MoveNextAsync);
            }
            else
            {
                Assert.NotNull(enumeratorInfo.MoveNextAsync);
                Assert.Equal("MoveNextAsync", enumeratorInfo.MoveNextAsync.Name);
                Assert.Equal(moveNextAsyncDeclaringType, enumeratorInfo.MoveNextAsync.DeclaringType);
                Assert.Empty(enumeratorInfo.MoveNextAsync.GetParameters());
            }

            if (disposeAsyncDeclaringType is null)
            {
                Assert.Null(enumeratorInfo.DisposeAsync);
            }
            else
            {
                Assert.NotNull(enumeratorInfo.DisposeAsync);
                Assert.Equal("DisposeAsync", enumeratorInfo.DisposeAsync.Name);
                Assert.Equal(disposeAsyncDeclaringType, enumeratorInfo.DisposeAsync.DeclaringType);
                Assert.Empty(enumeratorInfo.DisposeAsync.GetParameters());
            }
        }
    }
}

using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public static class DbSetMockExtensions
{
    public static Mock<DbSet<T>> CreateMockDbSet<T>(this IList<T> data) where T : class
    {
        var queryableData = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

        // Set up Add method to simulate adding an item
        mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(item => data.Add(item));

        // Set up Remove method to simulate removing an item
        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));

        return mockSet;
    }
}








using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Infrastructure.Annotations;
using ServiceDataContracts;

namespace StoreManager.Common
{
    internal static class TypeConfigurationExtensions
    {
        public static PrimitivePropertyConfiguration HasUniqueIndexAnnotation(
            this PrimitivePropertyConfiguration property,
            string indexName,
            int columnOrder)
        {
            var indexAttribute = new IndexAttribute(indexName, columnOrder) { IsUnique = true };
            var indexAnnotation = new IndexAnnotation(indexAttribute);

            return property.HasColumnAnnotation(IndexAnnotation.AnnotationName, indexAnnotation);
        }
    }

    public static class ExtenstionsMethods
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static bool IsRoot(this FoxeCategory foxeCategory)
        {
            return foxeCategory.ParentCategory == null;
        }

        public static bool IsLeaf(this FoxeCategory foxeCategory)
        {
            return foxeCategory.ChildCategories.Count == 0;
        }
    }
}

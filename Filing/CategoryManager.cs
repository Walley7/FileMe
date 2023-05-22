using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FileMe.Filing {

    public class CategoryManager {
        //================================================================================
        private List<Category>                  mCategories = new List<Category>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public CategoryManager() { }


        // CATEGORIES ================================================================================
        //--------------------------------------------------------------------------------
        public Category CreateCategory(bool detached = false) {
            Category category = new Category(this);
            if (!detached)
                AddCategory(category);
            return category;
        }

        //--------------------------------------------------------------------------------
        public void AddCategory(Category category) {
            // Name
            string name = category.Name;
            if (HasCategory(name)) {
                int counter = 2;
                while (HasCategory(name + $" ({counter})")) {
                    ++counter;
                }
                name = name + $" ({counter})";
            }

            // Add
            category.Name = name;
            mCategories.Add(category);
        }

        //--------------------------------------------------------------------------------
        public void DestroyCategory(Category category) {
            if (mCategories.Remove(category))
                category.Dispose();
        }

        //--------------------------------------------------------------------------------
        public Category Category(string name, bool caseSensitive = true) {
            // Name
            if (!caseSensitive)
                name = name.ToLower();

            // Find
            foreach (Category c in mCategories) {
                if ((caseSensitive ? c.Name : c.Name.ToLower()).Equals(name))
                    return c;
            }

            // Not found
            return null;
        }

        //--------------------------------------------------------------------------------
        public Category Category(XtraTabPage tabPage) {
            // Find
            foreach (Category c in mCategories) {
                if (c.TabPage == tabPage)
                    return c;
            }

            // Not found
            return null;
        }

        //--------------------------------------------------------------------------------
        public bool HasCategory(string name, bool caseSensitive = true) { return (Category(name, caseSensitive) != null); }
        public bool HasCategory(XtraTabPage tabPage) { return (Category(tabPage) != null); }

        //--------------------------------------------------------------------------------
        public void SortCategories() {
            mCategories.Sort((a, b) => a.Name.CompareTo(b.Name));
        }

        //--------------------------------------------------------------------------------
        public List<Category> Categories { get { return mCategories; } }
    }

}

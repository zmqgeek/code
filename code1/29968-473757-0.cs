    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    class Foo
    {
        public Foo() { Bars = new List<Bar>(); }
        public List<Bar> Bars { get; private set; }
    }
    class Bar
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Foo foo = new Foo();
            Bar bar = new Bar();
            bar.Name = "Fred";
            bar.DateOfBirth = DateTime.Today;
            foo.Bars.Add(bar);
            Application.EnableVisualStyles();
            using(Form form = new Form())
            using (Button btn = new Button())
            {
                form.Controls.Add(btn);
                btn.Text = "Edit";
                btn.Click += delegate
                {
                    MyHelper.EditValue(form, foo, "Bars");
                };
                Application.Run(form);
            }
        }
    }
    
    class MyHelper : IWindowsFormsEditorService, IServiceProvider, ITypeDescriptorContext
    {
        public static void EditValue(IWin32Window owner, object component, string propertyName) {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(component)[propertyName];
            if(prop == null) throw new ArgumentException("propertyName");
            UITypeEditor editor = (UITypeEditor) prop.GetEditor(typeof(UITypeEditor));
            MyHelper ctx = new MyHelper(owner, component, prop);
            if(editor != null && editor.GetEditStyle(ctx) == UITypeEditorEditStyle.Modal)
            {
                object value = prop.GetValue(component);
                value = editor.EditValue(ctx, ctx, value);
                if (!prop.IsReadOnly)
                {
                    prop.SetValue(component, value);
                }
            }
        }
        private readonly IWin32Window owner;
        private readonly object component;
        private readonly PropertyDescriptor property;
        private MyHelper(IWin32Window owner, object component, PropertyDescriptor property)
        {
            this.owner = owner;
            this.component = component;
            this.property = property;
        }
        #region IWindowsFormsEditorService Members
    
        public void CloseDropDown()
        {
            throw new NotImplementedException();
        }
    
        public void DropDownControl(System.Windows.Forms.Control control)
        {
            throw new NotImplementedException();
        }
    
        public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.Form dialog)
        {
            return dialog.ShowDialog(owner);
        }
    
        #endregion
    
        #region IServiceProvider Members
    
        public object GetService(Type serviceType)
        {
            return serviceType == typeof(IWindowsFormsEditorService) ? this : null;
        }
    
        #endregion
    
        #region ITypeDescriptorContext Members
    
        IContainer ITypeDescriptorContext.Container
        {
            get { return null; }
        }
    
        object ITypeDescriptorContext.Instance
        {
            get { return component; }
        }
    
        void ITypeDescriptorContext.OnComponentChanged()
        {}
    
        bool ITypeDescriptorContext.OnComponentChanging()
        {
            return true;
        }
    
        PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
        {
            get { return property; }
        }
    
        #endregion
    }
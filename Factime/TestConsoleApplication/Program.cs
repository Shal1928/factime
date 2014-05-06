using System;
using System.Reflection;
using System.Reflection.Emit;


namespace TestConsoleApplication
{
    public interface I
    {
        void M();
    }

    public class A
    {
        public virtual void M()
        {
            Console.WriteLine("In method A.M");
        }

        private string _stringProperty;
        public virtual string StringProperty
        {
            get
            {
                return _stringProperty;
            }
            set
            {
                Console.WriteLine("Important action");
                _stringProperty = value;
            }
        }

        public void ConsoleWriteln()
        {
            Console.WriteLine("Console");
        }
    }

    //// The object of this code example is to emit code equivalent to 
    //// the following C# code: 
    //// 
    //public class C : A, I
    //{
    //    public override void M()
    //    {
    //        Console.WriteLine("Overriding A.M from C.M");
    //    }

    //    // In order to provide a different implementation from C.M when  
    //    // emitting the following explicit interface implementation,  
    //    // it is necessary to use a MethodImpl. 
    //    // 
    //    void I.M()
    //    {
    //        Console.WriteLine("The I.M implementation of C");
    //    }
    //}

    class Program
    {
        static void Main()
        {
            var name = "DefineMethodOverrideExample";
            AssemblyName asmName = new AssemblyName(name);
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder mb = ab.DefineDynamicModule(name, name + ".dll");

            TypeBuilder tb = mb.DefineType("C", TypeAttributes.Public, typeof(A));


            tb.AddInterfaceImplementation(typeof(I));

            // Build the method body for the explicit interface  
            // implementation. The name used for the method body  
            // can be anything. Here, it is the name of the method, 
            // qualified by the interface name. 
            //
            MethodBuilder mbIM = tb.DefineMethod("I.M", MethodAttributes.Private | 
                                                        MethodAttributes.HideBySig |
                                                        MethodAttributes.NewSlot | 
                                                        MethodAttributes.Virtual |
                                                        MethodAttributes.Final, null, Type.EmptyTypes);

            ILGenerator il = mbIM.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "The I.M implementation of C"); //Pushes a new object reference to a string literal stored in the metadata.
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",new[] { typeof(string) }));
            il.Emit(OpCodes.Ret);

            // DefineMethodOverride is used to associate the method  
            // body with the interface method that is being implemented. 
            //

            tb.DefineMethodOverride(mbIM, typeof(I).GetMethod("M"));

            MethodBuilder mbM = tb.DefineMethod("M", MethodAttributes.Public | 
                                                     MethodAttributes.ReuseSlot |
                                                     MethodAttributes.Virtual | 
                                                     MethodAttributes.HideBySig, null, Type.EmptyTypes);
            il = mbM.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "Overriding A.M from C.M");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            il.Emit(OpCodes.Ret);




            //My attempt
            //***
            //***
            //***

            var targetProperty = typeof(A).GetProperty("StringProperty");
            const MethodAttributes getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual;
            FieldBuilder valueField = tb.DefineField("_" + targetProperty.Name, targetProperty.PropertyType, FieldAttributes.Private);
            MethodBuilder valuePropertyGet = tb.DefineMethod("get_StringProperty", getSetAttributes, null, new[] { targetProperty.PropertyType });

            il = valuePropertyGet.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, valueField); //load from field
            il.Emit(OpCodes.Ret);

            MethodBuilder valuePropertySet = tb.DefineMethod("set_StringProperty", getSetAttributes, null, new[] { targetProperty.PropertyType });

            il = valuePropertySet.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldstr, "Overridning StringProperty");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(A).GetProperty("StringProperty").GetSetMethod());
            //il.Emit(OpCodes.Stfld, valueField); //set to field
            il.Emit(OpCodes.Ret);

            //***
            //***
            //***


            Type tc = tb.CreateType();

            // Save the emitted assembly, to examine with Ildasm.exe.
            ab.Save(name + ".dll");

            A test = (A) Activator.CreateInstance(tc);

            MethodInfo mi = typeof(I).GetMethod("M");
            mi.Invoke(test, null);

            mi = typeof(A).GetMethod("M");
            mi.Invoke(test, null);


            //var targetProperty2 = test.GetType().GetProperty("StringProperty").GetSetMethod();
            //targetProperty2.Invoke(test, new[] {"1"});

            test.StringProperty = "1";
            test.ConsoleWriteln();
            Console.ReadLine();
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FIApp.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FIApp.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Collections.Generic;
        ///using System.Globalization;
        ///using System.Linq;
        ///using System.Text;
        ///using System.Threading.Tasks;
        ///using System.Windows.Data;
        ///
        ///namespace FIApp
        ///{
        ///    /**
        ///    * converter for aileron and elevator binding.
        ///    * aileron and elevator normal values are between -1 and 1.
        ///    * The conversion increases the range in order to emphasize the joystick&apos;s movement. 
        ///    */
        ///
        ///    class AileronElevatorConverter : IValueConverter
        ///    {
        ///        public object Conve [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AileronElevatorConverter {
            get {
                return ResourceManager.GetString("AileronElevatorConverter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Globalization;
        ///using System.Windows.Data;
        ///
        ///namespace FIApp
        ///{
        ///    // converter for airspeed binding: converts speed to degrees
        ///    class AirSpeedConverter : IValueConverter
        ///    {
        ///        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        ///        {
        ///            double newVal = (double)value;
        ///            //display any speed between 0 and 40 as 40
        ///            if (newVal &lt;= 40)
        ///            {
        ///                return 15.5;
        ///           [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AirSpeedConverter {
            get {
                return ResourceManager.GetString("AirSpeedConverter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Globalization;
        ///using System.Windows.Data;
        ///
        ///namespace FIApp
        ///{
        ///    // converter for altimeter binding: converts altitude to degrees
        ///    class AltimeterConverter : IValueConverter
        ///    {
        ///        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        ///        {
        ///            double newVal = (double)value;
        ///            //the parameter indicates which altimeter dial it is: 100 foot, 1000 foot or 10000 foot
        ///            int newParameter = Int32 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AltimeterConverter {
            get {
                return ResourceManager.GetString("AltimeterConverter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Globalization;
        ///using System.Windows.Data;
        ///
        ///namespace FIApp
        ///{
        ///    // converter for heading binding: changes the spin direction 
        ///    class HeadingConverter : IValueConverter
        ///    {
        ///        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        ///        {
        ///            double newVal = (double)value;
        ///            return newVal * -1;
        ///        }
        ///
        ///        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo c [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HeadingConverter {
            get {
                return ResourceManager.GetString("HeadingConverter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] LearnNormalDLL {
            get {
                object obj = ResourceManager.GetObject("LearnNormalDLL", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot;?&gt;
        ///
        ///&lt;PropertyList&gt;
        /// &lt;comment&gt;
        ///  &lt;![CDATA[
        ///    Usage:
        ///  
        ///    Recording: --generic=file,out,10,flight.out,playback_small
        ///    Playback:  --generic=file,in,10,flight.out,playback_small --fdm=null
        ///  ]]&gt;
        /// &lt;/comment&gt;
        ///
        /// &lt;generic&gt;
        ///
        ///  &lt;output&gt;
        ///   &lt;line_separator&gt;newline&lt;/line_separator&gt;
        ///   &lt;var_separator&gt;,&lt;/var_separator&gt;
        ///
        ///
        ///   &lt;!-- Flight Controls --&gt;
        ///   &lt;chunk&gt;
        ///    &lt;name&gt;aileron&lt;/name&gt;
        ///    &lt;type&gt;float&lt;/type&gt;
        ///    &lt;format&gt;%f&lt;/format&gt;
        ///    &lt;node&gt;/controls/flight/aileron[0]&lt;/nod [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string playback_small {
            get {
                return ResourceManager.GetString("playback_small", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0,0,0,0,0,0,0,0,0,0,0,0,0,0,63.98504421,-22.59248365,-9999,0,0.424,270.021362,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
        ///0,0,0,0,0,0,0,0,0,0,0,0,0,0,63.98504421,-22.59248365,-9999,0,0.424,270.021362,0,0,0,0,0,389.088776,1.193444,12,40,0,0,0,0,0,0,0,283.232513,0,0,0,0,0
        ///0,0,0,0,0,0,0,0,0,0,0,0,0,0,63.99183534,-22.60542555,-9999,0,0.424,180.018387,0,0,0,0,0,389.088776,1.193444,12,40,0,0,0,0,0,0,0,283.232513,0,0,0,0,0
        ///0,0,0,0,0,0,0,0,0,0,0,0,0,0,63.99183534,-22.60542555,-9999,0,0.424,180.018387,0,0,0,0,0,3 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string reg_flight {
            get {
                return ResourceManager.GetString("reg_flight", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Globalization;
        ///using System.Windows.Data;
        ///
        ///namespace FIApp
        ///{
        ///    /**
        ///     * converter for yaw, roll and pitch binding: converts value to degrees.
        ///     * yaw and roll range is from -180 to 180, pitch range is from -90 to 90.
        ///     */
        ///    class YawRollPitchConverter : IValueConverter
        ///    {
        ///        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        ///        {
        ///            double newVal = (double)value;
        ///            //the parameter i [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string YawRollPitchConverter {
            get {
                return ResourceManager.GetString("YawRollPitchConverter", resourceCulture);
            }
        }
    }
}

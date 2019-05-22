// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//
// 
// Description:
//              This is a helper class to facilate the storage of Security critical data ( aka "Plutonium")
//              It's primary purpose is to do put a [SecurityCritical] on all access to the data.     
//
//              What is "critical data" ? This is any data created that required an Assert for it's creation. 
//              As an example - the creation of an HwndWrapper during Dispatcher.Attach. 
//
//
//
//

using System ; 
using System.Security ;

#if WINDOWS_BASE
    using MS.Internal.WindowsBase;
#elif PRESENTATION_CORE
    using MS.Internal.PresentationCore;
#elif PRESENTATIONFRAMEWORK
    using MS.Internal.PresentationFramework;
#elif DRT
    using MS.Internal.Drt;
#else
#error Attempt to use FriendAccessAllowedAttribute from an unknown assembly.
using MS.Internal.YourAssemblyName;
#endif

namespace MS.Internal 
{
    [FriendAccessAllowed]
    internal struct SecurityCriticalData<T>
    {
        /// <SecurityNote>
        /// Critical - as this accesses _value which is Critical.
        /// Safe - as the caller already got the critical value.
        /// </SecurityNote>
        [SecurityCritical, SecurityTreatAsSafe]
        internal SecurityCriticalData(T value) 
        { 
            _value = value; 
        }

        // <SecurityNote>
        //    Critical "by definition" - this class is intended only to store critical data. 
        // </SecurityNote>
        internal T Value 
        { 
            [SecurityCritical]
            get 
            {     
                return _value; 
            } 
        }

        /// <SecurityNote>
        /// Critical - by definition as this is a wrapper for Critical data.
        /// </SecurityNote>
        [SecurityCritical]
        private T _value;
    }
}

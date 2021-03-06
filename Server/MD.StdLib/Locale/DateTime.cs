// Our DateTime Implimentation, indirectly compatable with System.DateTime
using System;
using System.Text;

namespace MD.StdLib.Locale {
	public class DateTime {
		public class PrecisionValueException : Exception {
			public PrecisionValueException( int value ) : base( $"Attempted assign {value}" ) {}
		}

		private int years;
		private decimal days;
		private int precision;

		public int Year { get => years; }
		public int Day { get => (int)Math.Truncate( days ); }
		public int Time { get => (int)Math.Floor( (days - Day) * (decimal)Math.Pow( 10, precision ) ); }
		public int Precision { get => precision; 
			set {
				if( value < 1 ) throw new PrecisionValueException( value );
				 precision = value; 
			}
		}

		public override string ToString() {
			// 5-place precision brings time to nearly seconds
			StringBuilder dtString = new StringBuilder();
			dtString.Append( $"{years:D4}.{Day:D3}." );
			int timeLength = (int)Math.Ceiling( Math.Log10( Time ) );
			if( precision > 0 ) {
				if( precision > timeLength ) {
					dtString.Append( '0', precision - timeLength );
				}
				dtString.Append( Time );
			}
			return dtString.ToString();
		}

		// Main Constructor: import DTS "Now" from System.DateTime
		public DateTime( int sigdig = 5 ) : this( System.DateTime.UtcNow, sigdig ) {} //< use runtime value as default
		public DateTime( System.DateTime dts, int sigdig = 5 ) {
			years = dts.Year;

			// Convert Time
			days = ((((decimal)dts.Millisecond / 1000 
						+ dts.Second) / 60
						+ dts.Minute ) / 60
						+ dts.Hour ) / 24
						+ dts.DayOfYear;

			precision = sigdig;
		}

		public static bool operator <( MD.StdLib.Locale.DateTime a, MD.StdLib.Locale.DateTime b ) {
			if( a.years != b.years ) {
				return a.years < b.years;
			} else if( a.days != b.days ) {
				return a.days < b.days;
			} else {
				return false;
			}
		}

		public static bool operator >( MD.StdLib.Locale.DateTime a, MD.StdLib.Locale.DateTime b ) {
			if( a.years != b.years ) {
				return a.years > b.years;
			} else if( a.days != b.days ) {
				return a.days > b.days;
			} else {
				return false;
			}
		}

		public override bool Equals( Object? obj ) {
			if( obj is MD.StdLib.Locale.DateTime ) {
				return this == (MD.StdLib.Locale.DateTime)obj;
			}
			return false;
		}

		public static bool operator ==( MD.StdLib.Locale.DateTime a, MD.StdLib.Locale.DateTime b ) {
			return a.years == b.years && a.days == b.days;
		}

		public static bool operator !=( MD.StdLib.Locale.DateTime a, MD.StdLib.Locale.DateTime b ) {
			return a.years != b.years || a.days != b.days;
		}

		public static bool operator >=( MD.StdLib.Locale.DateTime a, MD.StdLib.Locale.DateTime b ) {
			return a == b || a > b;
		}

		public static bool operator <=( MD.StdLib.Locale.DateTime a, MD.StdLib.Locale.DateTime b ) {
			return a == b || a < b;
		}
	}
}


﻿using System;
using System.Threading.Tasks;
using DecimalMath;
using NumSharp.Utilities;

namespace NumSharp.Backends
{
    public partial class DefaultEngine
    {
        public override NDArray Round(in NDArray nd, Type dtype) => Round(nd, dtype?.GetTypeCode());

        public override NDArray Round(in NDArray nd, int decimals, Type dtype) => Round(nd, decimals, dtype?.GetTypeCode());

        public override NDArray Round(in NDArray nd, NPTypeCode? typeCode = null)
        {
            if (nd.size == 0)
                return nd.Clone();

            var @out = Cast(nd, ResolveUnaryReturnType(nd, typeCode), copy: true);
            var len = @out.size;

            unsafe
            {
                switch (@out.GetTypeCode)
                {
#if _REGEN
                    %foreach except(supported_numericals, "Decimal"),except(supported_numericals_lowercase, "decimal")%
	                case NPTypeCode.#1:
	                {
                        var out_addr = (#2*)@out.Address;
                        for (int i = 0; i < len; i++) out_addr[i] = Converts.To#1(Math.Round(out_addr[i]));
                        return @out;
	                }
	                %
                    case NPTypeCode.Decimal:
	                {
                        var out_addr = (decimal*)@out.Address;
                        for (int i = 0; i < len; i++) out_addr[i] = (DecimalEx.Round(out_addr[i]));
                        return @out;
	                }
	                default:
		                throw new NotSupportedException();
#else
	                case NPTypeCode.Double:
	                {
                        var out_addr = (double*)@out.Address;
                        for (int i = 0; i < len; i++) out_addr[i] = Converts.ToDouble(Math.Round(out_addr[i]));
                        return @out;
	                }
	                case NPTypeCode.Single:
	                {
                        var out_addr = (float*)@out.Address;
                        for (int i = 0; i < len; i++) out_addr[i] = Converts.ToSingle(Math.Round(out_addr[i]));
                        return @out;
	                }
                    case NPTypeCode.Decimal:
	                {
                        var out_addr = (decimal*)@out.Address;
                        for (int i = 0; i < len; i++) out_addr[i] = (decimal.Round(out_addr[i]));
                        return @out;
	                }
	                default:
		                throw new NotSupportedException();
#endif
                }
            }
        }

        public override NDArray Round(in NDArray nd, int decimals, NPTypeCode? typeCode = null)
        {
            if (nd.size == 0)
                return nd.Clone();

            var @out = Cast(nd, ResolveUnaryReturnType(nd, typeCode), copy: true);
            var len = @out.size;

            unsafe
            {
                switch (@out.GetTypeCode)
                {
#if _REGEN
                    %foreach except(supported_numericals, "Decimal"),except(supported_numericals_lowercase, "decimal")%
	                case NPTypeCode.#1:
	                {
                        var out_addr = (#2*)@out.Address;
                        for (int i = 0; i < len; i++) out_addr[i] = Converts.To#1(Math.Round(out_addr[i], decimals));
                        return @out;
	                }
	                %
                    case NPTypeCode.Decimal:
	                {
                        var out_addr = (decimal*)@out.Address;
                        for (int i = 0; i < len; i++) out_addr[i] = (DecimalEx.Round(out_addr[i], decimals));
                        return @out;
	                }
	                default:
		                throw new NotSupportedException();
#else
                    case NPTypeCode.Double:
                        {
                            var out_addr = (double*)@out.Address;
                            for (int i = 0; i < len; i++) out_addr[i] = Converts.ToDouble(Math.Round(out_addr[i], decimals));
                            return @out;
                        }
                    case NPTypeCode.Single:
                        {
                            var out_addr = (float*)@out.Address;
                            for (int i = 0; i < len; i++) out_addr[i] = Converts.ToSingle(Math.Round(out_addr[i], decimals));
                            return @out;
                        }
                    case NPTypeCode.Decimal:
                        {
                            var out_addr = (decimal*)@out.Address;
                            for (int i = 0; i < len; i++) out_addr[i] = (decimal.Round(out_addr[i], decimals));
                            return @out;
                        }
                    default:
                        throw new NotSupportedException();
#endif
                }
            }
        }
    }
}

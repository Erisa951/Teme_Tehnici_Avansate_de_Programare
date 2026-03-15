using System;
using System.Collections.Generic;

namespace Tema_1.Payments;

public interface IPaymentService
{
    void RegisterPaymentMethod(IPaymentStrategy payment);

    IReadOnlyList<IPaymentStrategy> GetAvailableMethods();
}
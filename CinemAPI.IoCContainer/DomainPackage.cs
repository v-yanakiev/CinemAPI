using CinemAPI.Domain;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.AccessProjection;
using SimpleInjector;
using SimpleInjector.Packaging;
using CinemAPI.Data;
using CinemAPI.Data.Implementation;
using CinemAPI.Domain.NewReservation;
using CinemAPI.Domain.CancelReservation;
using CinemAPI.Domain.BuyTicket;

namespace CinemAPI.IoCContainer
{
    public class DomainPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INewProjection, NewProjectionCreation>();
            container.RegisterDecorator<INewProjection, NewProjectionMovieValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionUniqueValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionPreviousOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionNextOverlapValidation>();
            container.Register<IAccessProjection<int>, KeyAccessProjection>();
            container.Register<INewReservation, NewReservationCreation>();
            container.Register<ICancelReservation, CancelReservation>();
            container.Register<IBuyTicket, BuyTicket>();

        }
    }
}
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly ParkingContext db;
        public ParkingRepository(ParkingContext _db)
        {
            db = _db;
        }

        public List<ParkingPortal> GetDetail()
        {
            return (from p in db.parkingPortals
                    select new ParkingPortal
                    {
                        ReceiptNo = p.ReceiptNo,
                        DriverName = p.DriverName,
                        VehicleColor = p.VehicleColor,
                        VehicleNumber = p.VehicleNumber,
                        Brand = p.Brand,
                        Slot = p.Slot,
                        Status=p.Status,
                        ParkingDate = p.ParkingDate
                    }).ToList();
        }

        public object GetVehicleByNo(string number)
        {
            try
            {
                return (from table in db.parkingPortals
                        where (table.VehicleNumber == number)
                        select new ParkingPortal
                        {
                            DriverName=table.DriverName,
                            VehicleColor = table.VehicleColor,
                            Brand = table.Brand,
                            Status=table.Status,
                            Slot=table.Slot,
                            VehicleNumber = table.VehicleNumber,
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object GetVehicleByBrand(string name)
        {
            try
            {
                return (from table in db.parkingPortals
                        where (table.Brand == name)
                        select new ParkingPortal
                        {
                            DriverName=table.DriverName,
                            Brand = table.Brand,
                            Status=table.Status,
                            Slot=table.Slot,
                            VehicleColor = table.VehicleColor,
                            VehicleNumber = table.VehicleNumber
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object ParkStatus(ParkingStatus parkingStatus)
        {
            try
            {
                double total = db.parkingPortals.Where(p => p.ReceiptNo == parkingStatus.ReceiptNo)
                      .Select(i => (DateTime.Now.Subtract(i.ParkingDate).TotalMinutes)).Sum();

                parkingStatus.Date = DateTime.Now;
                parkingStatus.Charges =Convert.ToInt32( total * 10);

                var Status = db.parkingPortals.Find(parkingStatus.ReceiptNo);

                Status.Status = "UnPark";
                db.parkingPortals.Update(Status);
                db.Add(parkingStatus);
                db.SaveChanges();

                var data = (from p in db.parkingPortals
                            where p.ReceiptNo == parkingStatus.ReceiptNo
                            from q in db.ParkingStatuses
                            select new
                            {
                                p.ReceiptNo,
                                p.DriverName,
                                p.Brand,
                                p.VehicleColor,
                                p.Slot,
                                p.VehicleNumber,
                                p.ParkingDate,
                                parkingStatus.Date,
                                parkingStatus.Charges

                            }).FirstOrDefault();

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ParkingPortal AddData(ParkingPortal parkingPortal)
        {
            parkingPortal.Status = "Parked";
            parkingPortal.ParkingDate = DateTime.Now;
            db.Add(parkingPortal);
            db.SaveChanges();
            return parkingPortal;
        }
        }
    }
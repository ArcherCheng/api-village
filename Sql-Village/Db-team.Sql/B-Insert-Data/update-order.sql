-- use foodpos2
update OrderMaster set orderDate = DATEADD(second,orderId * 20 , GETDATE()); 
update OrderMaster set orderDate = DATEADD(minute,-15 , orderDate); 
update OrderDetail set IsCookOver = 0, CookStep = 0;
update OrderMaster set IsCookOver = 0, OrderStep = 0, IsCheckout=0;

-- update OrderMaster set TableNo = SUBSTRING( convert(nvarchar(4),TakeNo),4,1)

-- create view View_OrderDetailSum as 
-- select orderId,sum(salePrice) SalePriceSum,sum(offPrice) OffPriceSum,sum(addonPrice) AddonPriceSum from orderDetail group by OrderId

-- update orderMaster set SalePriceSum = b.SalePriceSum, OffPriceSum = b.OffPriceSum, AddonPriceSum = b.AddonPriceSum, OrderPriceSum = b.SalePriceSum - b.OffPriceSum + b.AddonPriceSum ,
-- InvoiceAmt = a.OrderPriceSum - a.PromotionAmt
-- from OrderMaster a, View_OrderDetailSum b where a.OrderId = b.OrderId



select * from OrderMaster
select * from OrderDetail

drop table temp_orderMaster
drop table temp_orderDetail
drop table temp_orderDetailAddon
GO

select * into temp_orderMaster from OrderMaster
select * into temp_orderDetail from OrderDetail
select * into temp_orderDetailAddon from OrderDetailAddON
GO

insert into OrderMaster(customerId,OrderDate,OrderType,tableNo,OrderPeoples,TakeNo,OrderAmt,
    promotionName,promotionAmt,InvoiceAmt,IsCookOver,OrderStep,IsCheckOut)
select customerId,OrderDate,OrderType,tableNo,OrderPeoples,TakeNo, OrderPriceSum,
    promotionName,promotionAmt,InvoiceAmt,IsCookOver,OrderStep,IsCheckOut
from Temp_OrderMaster



insert into OrderDetail(OrderId,FoodId,Qty,SalePrice,OffPrice,AddonPrice,DetailAmt,IsCookOver,CookStep,seatNo)
select OrderId,FoodId,Qty,SalePrice,OffPrice,AddonPrice,(SalePrice - OffPrice + AddonPrice)*Qty,IsCookOver,CookStep,seatNo from temp_orderDetail


insert into OrderDetailAddon(detailId,AddonId)
select detailId,AddonId from temp_orderDetailAddon


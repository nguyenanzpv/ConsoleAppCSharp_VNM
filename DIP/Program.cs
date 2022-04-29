/*
 * 1. Các module cấp cao không nên phụ thuộc vào các modules cấp thấp. 
   Cả 2 nên phụ thuộc vào abstraction.
    2. Interface (abstraction) không nên phụ thuộc vào chi tiết, mà ngược lại.
    ( Các class giao tiếp với nhau thông qua interface, không phải thông qua implementation.)
 Dependence: Cac thanh phan phu thuoc
 Class A chua class B va C -> B va C la cac dependence -> Module cap cao phu thuoc vao module cap thap => ko nen
 Class -> ke thua abstract class -> ke thua interface
 Phu thuoc truc tiep: class A giao tiep truc tiep class B
 Dao nguoc su phu thuoc:class A va B cung giao tiep voi interface
 */

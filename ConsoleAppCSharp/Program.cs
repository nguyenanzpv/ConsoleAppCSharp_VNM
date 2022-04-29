// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
/*
1. Data Type in C#:
    - Number:kieu gia tri -> luu tru gia tri tren stack 
        + primitive(nguyen thuy) int, long, byte, sbyte, double, ...nint,nuint //nint: n la native dung cho tu chay trong ban 32bit
        + char: 2 byte: ky tu latin ma ascii chi can dung toi da 2 byte
    - Object: kieu du lieu tham chieu. Vung nho cua bien chi chua dia chi cua du lieu luu tren stack. Gia tri luu tren heap
        + Object
        + string
        + Anonymous
            Anonymous Type dùng để đóng gói cách thuộc tính chỉ đọc (read-only properties) vào một đối tượng mà không cần xác định rõ ràng loại (type) của nó.        
        + dynamic: dynamic <tên biến>;
            Các đối tượng thuộc kiểu dynamic sẽ không xác định được kiểu cho đến khi chương trình được thực thi. Tức là trình biên dịch sẽ bỏ qua tất cả lỗi về cú pháp, việc kiểm tra này sẽ thực hiện khi chương trình thực thi.
            Ko Phải khởi tạo giá trị khi khai báo
            Sử dụng để làm kiểu trả về hoặc tham số cho hàm
            Có khả năng ép kiểu qua lại với các kiểu dữ liệu khác
            Xác định khi chương trình thực thi
        + Var là một từ khóa để khai báo một cách ngầm định kiểu dữ liệu và kiểu anonymous (kiểu anonymous sẽ được trình bày ở những bài sau).
            Phải khởi tạo giá trị khi khai báo
            Ko Sử dụng để làm kiểu trả về hoặc tham số cho hàm
            Ko Có khả năng ép kiểu qua lại với các kiểu dữ liệu khác
            Xác định ngay tại khai báo thông qua giá trị được gán vào
2. Variables
3. Coments
4. Constant/readonly
5. Operator:+-* /..
6. Lambda: viet code ngan gon thong qua => tang hieu nang -> tao ra cac ham vo danh anonymous
    - Lambda expression: bieu thuc lambda int k=x => x+x;
    - Lambda statement
7. Cau truc lap trinh
    - If statement, if..else, nested if
    - Switch: switch statement, switch expression(feature in c# 8)
    - Loop: while, do while, for, foreach
8.  Cau truc du lieu va giai thuat: geeks for geek -> althorium
    - Static array
    - List
    - Array List
    - Linked
    - Search
    - Sort: Merge sort
9.  C# namespace
    namespace Com
    {
        namespace Edu{

        }
    }
10. Coding Convention
11. Properties vs Fields:cùng chứa dữ liệu
    - Properties:
        + Có thể gán và nhận dữ liệu
        + Ko thể sử dụng với 2 từ khóa ref và out
        + Định nghĩa bởi 2 biểu tức get và set
        + Chứa các hành động xử lý với field
        + Thường để public
    - Fields:
        + Chỉ chứa dữ liệu
        + Là biến nên có thể sử dụng với ref và out
        + Ko có get và set
        + Ko thể nhập và xuất dữ liệu
        + Thường để private, truy xuất thông qua properties

*/

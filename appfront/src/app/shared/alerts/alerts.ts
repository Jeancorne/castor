import Swal from "sweetalert2";

export class Alerts {

    static warning(title: string, text: string, buttonText: string): void {
        Swal.fire({
            icon: 'error',
            title,
            html: text,
            confirmButtonText: buttonText,
            confirmButtonColor: '#828282',
            heightAuto: false,
        });
    }
    static success(title: string, text: string, buttonText: string): void {
        Swal.fire({
            icon: 'success',
            title,
            html: text,
            confirmButtonText: buttonText,
            heightAuto: false,
        });
    }

    static confirmData(titulo: string) {
        return Swal.fire({
            title: titulo,
            showCancelButton: true,
            confirmButtonText: 'Si',
        }).then((result: any) => {
            if (result.isConfirmed) {
                return true;
            } else if (result.isDenied) {
                return false;
            }
            return null;
        })
    }

    static GetErrors(data: any) {
        let error: any = "";
        if (data?.status == 401 || data?.status == 405) {
            error = data?.statusText;
            return error;
        }
        if (data?.error?.code == 500) {
            error = data?.error?.message;
            return error;
        }
        if (data?.error?.errors != null) {
            for (const [value] of Object.entries(data?.error?.errors)) {
                error = value;
                break;
            }
            return error;
        }
        if (data?.error?.code == 400) {
            error = data?.error?.message;
            return error;
        }
        if (data?.status == 400) {
            error = data?.error?.message;
            return error;
        }
        if (data?.code == 400) {
            for (const [value] of Object.entries(data?.data)) {
                error = value;
                break;
            }
            return error;
        }
        if (data?.error?.code == 400) {
            error = data?.error?.message;
            return error;
        }
        if (data?.isSuccess == false) {
            error = data.message;
            return error;
        }
        if (data?.error?.code == 400) {
            if (data?.error?.data != null) {
                for (const [value] of Object.entries(data?.error?.data)) {
                    error = value;
                    break;
                }
                return error;
            }
        }
        return error;
    }
}
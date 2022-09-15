import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { ToastService } from 'src/Services/ToastService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  public ngUnsubscribe = new Subject<void>();
  public imageUrl!: string;

  public productForm = new UntypedFormGroup({
    id: new UntypedFormControl(
      { value: '', disabled: false }
    ),
    name: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    ),
    image: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    ),
    desc: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    ),
    stock: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    ),
    status: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    ),
    price: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    )
  });

  constructor(
    private _httpClient: HttpClient,
    private _toastService: ToastService
  ){}

  InsertProduct()
  {
    if (this.productForm.value == null)
    {
      this._toastService.addToast('warning', 'Warning', 'Please enter all product data.')
    }
    const headers = new HttpHeaders().set('Content-Type', 'application/json')
    this._httpClient.post('http://localhost:5000/api/Product/', this.productForm.value, {headers}).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
      next: (response: any) => {
        this._toastService.addToast('success', 'Success', 'Product created successfully.');
      },
      error: (error: any) => {
        this._toastService.addToast('warning', 'Warning', 'It was not possible to complete the creation of the product.');
      }
    });
  }

  ChangeImageUrl(isSelect: boolean)
  {
    console.log('ola')
    if (isSelect)
    {
      this.productForm.get('image')?.setValue(this.imageUrl);
    }
    else
    {
      this.productForm.get('image')?.setValue('');
    }
  }
}

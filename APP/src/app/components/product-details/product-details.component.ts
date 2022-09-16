import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.less']
})
export class ProductDetailsComponent implements OnInit {
  public ngUnsubscribe = new Subject<void>();
  public errorMessage = false;
  public errorMessageUpdate = false;
  public showDialog = false;
  public productDetails: any;
  public imageUrl!: string;
  public productDetailsForm = new UntypedFormGroup({
    id: new UntypedFormControl(
      {value: '', disabled: false}
    ),
    name: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    ),
    image: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    ),
    description: new UntypedFormControl(
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

  constructor(private _activatedRoute: ActivatedRoute, private _httpClient: HttpClient, private _router: Router) { }

  ngOnInit(): void {
    this._activatedRoute.params.pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (params: any) => {
        this.GetProductById(Object(params).id);
      }
    );
  }

  GetProductById(id: any)
  {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this._httpClient.get('http://localhost:5000/api/Product/Get/' + id, {headers}).pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (response: any) => {
        this.productDetails = response;
      }
    );
  }

  DeleteProduct()
  {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this._httpClient.delete('http://localhost:5000/api/Product/Delete/' + this.productDetails.id, {headers}).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
      next: (response: any) => {
        this.errorMessage = false;
        this._router.navigate(['products']);
      },
      error: (error: any) => {
        this.errorMessage = true;
      }
    });
  }

  UpdateProduct()
  {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this._httpClient.put('http://localhost:5000/api/Product/Update', this.productDetailsForm.value, {headers}).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
      next: (response: any) => {
        this.showDialog = false;
        this.errorMessageUpdate = false;
        this.GetProductById(this.productDetailsForm.get('id')?.value);
      },
      error: (error: any) => {
        this.showDialog = false;
        this.errorMessageUpdate = true;
      }
    });
  }
  
  ChangeImageUrl(isSelect: boolean)
  {
    if (isSelect)
    {
      this.productDetailsForm.get('image')?.setValue(this.imageUrl);
    }
    else
    {
      this.productDetailsForm.get('image')?.setValue('');
    }
  }

  ShowDialogUpdate(){
    this.productDetailsForm.reset();
    this.productDetailsForm.patchValue(this.productDetails);
    this.imageUrl = this.productDetails.image;
    this.showDialog = true;
  }
}

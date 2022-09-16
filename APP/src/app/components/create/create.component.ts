import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.less']
})
export class CreateComponent implements OnInit {
  public ngUnsubscribe = new Subject<void>();
  public imageUrl!: string;
  public errorMessage: boolean = false;

  public productForm = new UntypedFormGroup({
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
    price: new UntypedFormControl(
      { value: '', disabled: false }, Validators.compose([Validators.required])
    )
  });

  constructor(
    private _httpClient: HttpClient
  ){}

  ngOnInit(): void {
  }

  InsertProduct()
  {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this._httpClient.post('http://localhost:5000/api/Product/Create', this.productForm.value, {headers}).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
      next: (response: any) => {
        this.productForm.reset();
        this.productForm.get('image')?.setValue('');
        this.imageUrl = '';
        this.errorMessage = false;
      },
      error: (error: any) => {
        this.errorMessage = true;
      }
    });
  }

  ChangeImageUrl(isSelect: boolean)
  {
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

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CellClickedEvent, ColDef } from 'ag-grid-community';
import { Observable, Subject } from 'rxjs';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.less']
})
export class ProductsComponent implements OnInit {
  public ngUnsubscribe = new Subject<void>();
  public products$!: Observable<any[]>;

  colDefs: ColDef[] = [
    { field: 'id'},
    { field: 'name'},
    { field: 'image'},
    { field: 'description'},
    { field: 'stock'},
    { field: 'status'},
    { field: 'price'}
  ];

  public defaultColDef: ColDef = {
    sortable: true,
    filter: true,
  };

  constructor(private _httpClient: HttpClient, private _router: Router) {}

  ngOnInit() {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.products$ = this._httpClient.get<any[]>('http://localhost:5000/api/Product/Get', {headers});
  }

  onCellClicked(event: any){
    this._router.navigate(['products/' + event.toString()]);
  }

}

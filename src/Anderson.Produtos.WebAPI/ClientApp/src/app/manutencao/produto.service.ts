
import { Inject, Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { HttpClient } from "@angular/common/http";
import { Produto } from './produto.model';
import { Observable, EMPTY } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: "root",
})
export class ProdutoService {
  constructor(private snackBar: MatSnackBar,
    private http: HttpClient,
    @Inject('BASE_URL')
    private baseUrl: string) {
  }

  //TODO: change the way the post was implemented to FromBody
  create(produto: Produto): Observable<Produto> {
    console.log('this.baseUrl', `${this.baseUrl}api/Produtos`);
    const formData = new FormData();

    for (let key in produto) {
      if (key !== 'imagemArquivo')
        formData.append(key, produto[key]);
    }

    formData.append('imagemArquivo', produto.imagemArquivo);

    return this.http.post<Produto>(`${this.baseUrl}api/Produtos`, formData);
  }

  read(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseUrl);
  }

  readById(id: number): Observable<Produto> {
    const url = `${this.baseUrl}api/Produtos/${id}`
    return this.http.get<Produto>(url);
  }

  update(produto: Produto): Observable<Produto> {
    console.log('produto', produto)
    const url = `${this.baseUrl}api/Produtos/${produto.id}`
    return this.http.put<Produto>(url, produto);
  }

  delete(id: number): Observable<Produto> {
    const url = `${this.baseUrl}api/Produtos/${id}`
    return this.http.delete<Produto>(url);
  }
}

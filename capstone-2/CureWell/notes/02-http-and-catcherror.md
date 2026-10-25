# 02 — HttpClient and catchError

When you need this: every method in `curewell.service.ts`.

## Syntax

```typescript
return this.http
  .get<ResultType>(url)
  .pipe(catchError(this.errorHandler));

return this.http.post<boolean>(url, body).pipe(catchError(this.errorHandler));
return this.http.put<boolean>(url, body).pipe(catchError(this.errorHandler));

return this.http
  .request<boolean>('delete', url, { body: doctor })
  .pipe(catchError(this.errorHandler));
```

## Where each part goes

| Piece | Role |
|---|---|
| `this.http` | injected `HttpClient` |
| `<Doctor[]>` / `<boolean>` | type of the JSON body |
| first string | full action URL |
| second argument of `post`/`put` | JSON body |
| `{ body: doctor }` | body for DELETE (the verb itself is the first argument of `request`) |
| `pipe(catchError(...))` | PS: call `errorHandler` on any HTTP exception |

`errorHandler` must return an observable error, not throw a plain value:

```typescript
errorHandler(error: HttpErrorResponse) {
  return throwError(error.message || 'ERROR');
}
```

## This project

```typescript
private readonly baseUrl = "http://localhost:5109/api/CureWell/"

this.http.get<Doctor[]>(this.baseUrl + 'GetDoctors')
this.http.get<Specialization[]>(this.baseUrl + 'GetSpecializations')
this.http.get<Surgery[]>(this.baseUrl + 'GetAllSurgeryTypeForToday')
this.http.post<boolean>(this.baseUrl + 'AddDoctor', doctor)
this.http.put<boolean>(this.baseUrl + 'UpdateDoctorDetails', doctor)
this.http.put<boolean>(this.baseUrl + 'UpdateSurgery', surgery)
this.http.request<boolean>('delete', this.baseUrl + 'DeleteDoctor', { body: doctor })
```

Action names are the controller method names. Route template is `api/[controller]/[action]`, controller class is `CureWellController`, so the segment is `CureWell`.

## Exam variations

1. `http.delete(url, { body })` — same idea as `request('delete', …)` on newer Angular.
2. `catchError(err => this.errorHandler(err))` if `this` is lost. Passing `this.errorHandler` works here because the method does not use `this`.
3. `throwError(() => error.message)` — RxJS 7+ factory form. The committed code uses `throwError(message)`.
4. Forgetting `pipe(catchError)` means the component `error` callback never sees this handler’s message.

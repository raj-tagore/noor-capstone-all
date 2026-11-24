# Question 4 — Delete a doctor

View Doctors needs a second button, Remove Doctor Details. It deletes that row and tells the user whether the delete worked.

From the `removeDoctor` row:

- Call the service with the doctor.
- Store the boolean in `status`.
- If it is true, `alert('Doctor detailed deleted successfully!')`.
- If it is false, `alert("Doctor's name not deleted")`.
- On failure, set `errorMsg` to `Some error occured`.

The service method is `deleteDoctor(doctor)`. It calls `DeleteDoctor` and returns `boolean`. The body is the whole doctor, not an id in the URL.

Already used: `subscribe`, `alert`, `(click)`, GET and POST and PUT.

## DELETE with a body

`delete` usually only needs a URL (`/DeleteDoctor/1001`). This action’s parameter is a `Doctor`, so the object has to travel in the body. `HttpClient.delete` accepts that as an options object:

```typescript
deleteDoctor(doctor: Doctor): Observable<boolean> {
  return this.http
    .delete<boolean>(this.baseUrl + 'DeleteDoctor', { body: doctor })
    .pipe(catchError(this.errorHandler));
}
```

`{ body: doctor }` is the only new argument. The pipe matches the earlier methods.

The same request written with `request` names the verb as a string. Use this if `delete`’s options object is awkward in the version you are on:

```typescript
this.http.request<boolean>('delete', this.baseUrl + 'DeleteDoctor', { body: doctor })
```

`request` can express every verb. The first argument is `'get'`, `'post'`, `'put'`, or `'delete'`. Prefer `.delete` here. It is the same options object.

A URL id would look like this, and it is the wrong shape for this API:

```typescript
this.http.delete<boolean>(this.baseUrl + 'DeleteDoctor/' + doctor.doctorId)
```

The action would receive no `Doctor`, and the id would never arrive.

## The component

```typescript
removeDoctor(doctor: Doctor) {
  this._curewellService.deleteDoctor(doctor).subscribe({
    next: (response) => {
      this.status = response;
      if (response) {
        alert('Doctor detailed deleted successfully!');
        this.getDoctor();
      } else {
        alert("Doctor's name not deleted");
      }
    },
    error: () => {
      this.errorMsg = 'Some error occured';
    }
  });
}
```

The true-branch sentence is the table’s wording, `detailed`, not `details`. The error string is `occured`, the spelling on this row. This row does not ask for a console line, so there is no `complete` handler.

`this.getDoctor()` is question 1’s method. Calling it after a successful delete runs the GET again and replaces `doctorList`, so the row disappears without a full page reload. The table does not require that call. Without it, the dialog still appears and the row stays until the next visit. Call it so the screen matches the database.

The false branch does not reload. Nothing was deleted.

The error callback ignores the argument and assigns the fixed sentence. Question 1 stored the error object itself (`this.errorMsg = error`). This row tells you the exact string.

## The button

Same `(click)` form as Edit Doctor Details. Add it in the same `*ngFor` row. The Action header already has `colspan="2"` for these two cells.

```html
<td>
  <button class="btn" (click)="removeDoctor(doctor)">Remove Doctor Details</button>
</td>
```

The method table says Remove. A label of Delete Doctor Details is the same click with different text. Use Remove so it matches the table.

Click Remove on a doctor who has no surgeries depending on them. The dialog says `Doctor detailed deleted successfully!` and the row is gone. Click Remove when the API rejects the delete: `Doctor's name not deleted`, and the row remains.

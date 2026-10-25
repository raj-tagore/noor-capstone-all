# 03 — subscribe

When you need this: the component receives the service `Observable`. The HTTP call does not run until `subscribe`.

## Syntax

```typescript
this._curewellService.someMethod(...).subscribe({
  next: (response) => {
    // success value
  },
  error: (error) => {
    // catchError / HTTP failure
  },
  complete: () => {
    // runs after a successful next, not after error
  }
});
```

## Where the PS text goes

| PS line | Handler |
|---|---|
| “in case of successful execution, store the response in …” | `next` |
| “if the response is true, alert … else alert …” | `if` inside `next` (`response` is the `boolean`) |
| “in case of any error, store … / alert …” | `error` |
| “on completion, console.log …” | `complete` |

`next` receives the JSON. `error` receives whatever `errorHandler` forwarded (`error.message`).

## Pattern used in the commits

List fetch:

```typescript
next: (res) => { this.doctorList = res; },
error: (error) => { this.doctorList = null; this.errorMsg = error; }
```

Boolean + navigate:

```typescript
next: (response) => {
  this.status = response;
  if (response) { alert('...successfully...'); }
  else { alert('...not updated'); }
  this.router.navigate(['/viewDoctors']);
},
error: (error) => {
  this.errorMsg = error;
  alert('Some error occurred');
  this.router.navigate(['/viewDoctors']);
},
complete: () => { console.log('...'); }
```

## Exam variations

1. `subscribe(data => {}, err => {})` — two callbacks, no `complete`.
2. Putting `console.log` inside `next` still prints on success. The table’s “on completion” maps to `complete`, which is what add/update commits use.
3. `complete` does not run when `error` runs. A failure message that must always print belongs in both handlers, or after the call only if you do not need the HTTP result.
4. Do not assign the list inside `error` to `[]` if the table says `null`.

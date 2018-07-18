# Leaking-ListView
Demo project reproducing ListView cells leak when in Recycle mode and ScrollTo is used.
When such ListView had MenuItem with bindings then NRE happens after leaving the page and setting BindingContext to null.
When MenuItem does not have any bindings then we end up with just leaked cells.

# Blackbird.io termbase.io

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

termbase.io is a fully featured translation solution for enterprises and LSPs, with the focus on leveraging AI & corporate language to produce best quality and human verified results.

## Before setting up

Before you can connect you need to make sure that:

- You must receive personal Auth-Token from the termbase.io team

## Connecting

1. Navigate to apps and search for LanguageWire
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'termbase.io'.
4. Fill 'API token' field with your Auth token
5. Click _Connect_

![connection](image/README/connection.png)

## Actions

### Language actions

- **Get languages** - Get all languages

### Termbase actions

- **Get termbases** - Get all termbases
- **Get termbase** - Get termbase by uuid
- **Export termbase** - Export termbase in specified format (by default it will be exported in tbx)

### Term import actions

- **Create term import** - Create a term import and start it
- **Get term import** - Get a term import by uuid
- **Delete term import** - Delete a term import by uuid
- **Export termbase from term import** - Download termbase from term import in specified format (by default it will be exported in tbx)

## Webhooks

- **On term changed** - Triggered when a term is changed
- **On term import finished** - Triggered when a term import is finished

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
